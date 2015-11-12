using UnityEngine;
using System.Collections;

// The Tile Grid that will be filled Up by the 

[SelectionBase]
public class TileGrid : MonoBehaviour
{

    
    public int TilesPerUnit = 10;
    float TileSize;

    public int GridHeight = 10;
    public int GridWidth = 10;

    int TilesPerWidth;
    int TilesPerHeight;

    private GameObject[] TileArray;
    public GameObject TilePrefab;

    public void Awake()
    {
       // Set Tile Size
       TileSize = 1f / TilesPerUnit;

       TilesPerWidth = GridWidth * TilesPerUnit;
       TilesPerHeight = GridHeight * TilesPerUnit;

        // Creates TileArray
        TileArray = new GameObject[TilesPerHeight * TilesPerWidth];

        // Creates TileGrid
        CreateGrid();
    }

    // Creates the Tile Grid
    void CreateGrid()
    {
        int TileArrayIndex= 0;
        for (int y = 0; y < TilesPerHeight; y++)
        {
            for (int x = 0; x < TilesPerWidth; x++)
            {
                // Creates the Tile at grid position
                CreateTile(TileArrayIndex, x, y);

                TileArrayIndex++;
            }
        }
    }
    // Creates tile at Given Grid Position and Assign it to its Array Position
    private void CreateTile(int TileArrayIndex, int x, int y)
    {
        // Creates Tile at position.
        GameObject Tile = Instantiate(TilePrefab) as GameObject;
        Tile.transform.parent = transform;
        Tile.transform.localPosition = new Vector3((x + 0.5f) * TileSize, (y + 0.5f) * TileSize);
        Tile.transform.localScale = Vector3.one * TileSize;

        // Set Tile type to Unassigned Type
        Tile.GetComponent<Tile>().Type = TileType.Unassigned;
        
        // Assign Tile to its Position
        TileArray[TileArrayIndex] = Tile;
        
    }
}
