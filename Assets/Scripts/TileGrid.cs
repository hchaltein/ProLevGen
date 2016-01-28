using UnityEngine;
using System.Collections;

// The Tile Grid that will be filled Up by the 
public class TileGrid : MonoBehaviour
{
    int TilesPerUnit = 10;
    float TileSize;

    int GridHeight = 10;
    int GridWidth = 10;

    public int TilesPerWidth;
    public int TilesPerHeight;

    public GameObject[] TileArray;
    public GameObject TilePrefab;

    // Set Grid Size and proportions
    public void SetGridSize(int _GridHeight, int _GridWidth, int _TilesPerUnit)
    {
        GridHeight = _GridHeight;
        GridWidth = _GridWidth;
        TilesPerUnit = _TilesPerUnit;

        // Set Tile Size
        TileSize = 1f / TilesPerUnit;

        // Calculate Grid's sizes in 
        TilesPerWidth = GridWidth * TilesPerUnit;
        TilesPerHeight = GridHeight * TilesPerUnit;

        // Creates TileArray
        TileArray = new GameObject[TilesPerHeight * TilesPerWidth];
    }

    // Creates the Tile Grid
    public void CreateGrid()
    {
        int TileArrayIndex= 0;
        for (int y = 0; y < TilesPerHeight; y++)
        {
            for (int x = 0; x < TilesPerWidth; x++)
            {
                // Creates the Tile at grid position
                CreateTile(TileArrayIndex, x, y);
                // Increment Index
                TileArrayIndex++;
            }
        }
    }

    // Creates tile at given grid position and assign it to its Array Position
    private void CreateTile(int TileArrayIndex, int x, int y)
    {
        // Creates Tile at position.
        GameObject Tile = Instantiate(TilePrefab) as GameObject;
        Tile.transform.parent = transform;
        Tile.transform.localPosition = new Vector3((x + 0.5f) * TileSize, (y + 0.5f) * TileSize);
        Tile.transform.localScale = Vector3.one * TileSize;

        // Set Tile type to Wall and give it an index.
        Tile.GetComponent<Tile>(). SetTileType(TileType.Wall);
        Tile.GetComponent<Tile>().Index = TileArrayIndex;

        // Assign Tile to its Position
        TileArray[TileArrayIndex] = Tile;
    }

    // Creates a room from a Bottom Left and a Upper Right index.
    public void CreateRoomFromIndex(int BotLeftTileIndex, int UpRightTileIndex)
    {
        int BotLeftMod, BotLeftDiv;
        int UpRightMod, UpRightDiv;

        // BottomLeft Tile "X(%) Y(/) coordinates" 
        BotLeftMod = BotLeftTileIndex % TilesPerWidth;
        BotLeftDiv = BotLeftTileIndex / TilesPerWidth;

        // UpperRight Tile "X(%) Y(/) coordinates" 
        UpRightMod = UpRightTileIndex % TilesPerWidth;
        UpRightDiv = UpRightTileIndex/ TilesPerWidth;

        // UpRight tile to the LEFT or BELOW BottomLeft tile
        if (UpRightDiv <= BotLeftDiv +1 || UpRightMod <= BotLeftMod +1)
        {
            Debug.Log("Invalid Room Coordinates");
            return;
        }

        // Iterate through grid selecting the room tiles.
        for (int i = BotLeftTileIndex; i <= UpRightTileIndex; i++)
        {
            #region Room Interior
            // Select Tiles Rows (Division)
            if (((BotLeftDiv < ( i/ TilesPerWidth)) && ((i / TilesPerWidth) < UpRightDiv)) &&
            // Select Tiles Columns (Module)
                (BotLeftMod < ( i %TilesPerWidth)) && ((i % TilesPerWidth) < UpRightMod))
            {
                TileArray[i].GetComponent<Tile>().SetTileType(TileType.Room);
            }
            #endregion

            #region Room Walls
            // Select Tiles Rows (Division) with excess Columns
            else if (((BotLeftDiv == (i / TilesPerWidth)) || ((i / TilesPerWidth) <= UpRightDiv)) &&
            // Exclude excess Tiles Columns (Module)
                !((i%TilesPerWidth) < BotLeftMod) && !(UpRightMod < (i % TilesPerWidth)))
            {
                TileArray[i].GetComponent<Tile>().SetTileType(TileType.WallBorder);
            }
            #endregion
        }
    }

    // Creates a corridor taht connect the center of two Rooms
    public void CreateCorridorFromIndex (int Room1Center, int Room2Center)
    {
        int TopTile, BotTile;
        int CornerTile, CornerDeltaX;
        int StartTile, EndTile;

        // Figure out which is the topmost Room. Room indexes grow from left to right, bot to top.
        if (Room1Center > Room2Center)
        {
            TopTile = Room1Center;
            BotTile = Room2Center;
        }
        else
        {
            TopTile = Room2Center;
            BotTile = Room1Center;
        }

        // Calculate corridor corner. All corridors are L-shaped.
        CornerDeltaX = TopTile % TilesPerWidth - BotTile% TilesPerWidth;
        CornerTile = BotTile + CornerDeltaX;

        if (CornerTile < BotTile)
        {  // Corner Tile is to the left of Bottom tile
            StartTile = CornerTile;
            EndTile = BotTile;
        }
        else
        {  // Corner Tile is to the right of Bottom tile
            StartTile = BotTile;
            EndTile = CornerTile;
        }

        // Creates horizontal part of corridor. Tiles are set from left to right.
        for (int i = StartTile; i <= EndTile;)
        {
            // Creates actual corridor
            TileArray[i].GetComponent<Tile>().SetTileType(TileType.Corridor);
            i++;
        }

        StartTile = CornerTile;
        EndTile = TopTile;

        // Creates Vertical Part of Corridor. Tiles are set from bottom to top.
        for (int i = StartTile; i <= EndTile;)
        {
            TileArray[i].GetComponent<Tile>().SetTileType(TileType.Corridor);
            i += TilesPerWidth;
        }
    }
}
