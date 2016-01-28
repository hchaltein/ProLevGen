using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum CorridorConnect
{
    AllToAll,
    Max1PerRoom,
    Max2PerRoom,
    Max3PerRoom
}

// Requirements
[RequireComponent(typeof(TileGrid))]
public class LevelGenerator : MonoBehaviour {

    // Grid Varialbes
    TileGrid grid;
    public int GridWidth;
    public int GridHeight;
    public int TilesPerUnit;

    [SerializeField]
    [Range(0,999)]
    int RNGSeed;

    [SerializeField]
    [Range(0, 50)]
    int TotalRooms;

    [SerializeField]
    CorridorConnect CorridorType;

    System.Random RNG;

    List<Room> RoomList;

    // Use this for initialization
    void Awake()
    {
        // Starting Conditions
        GridWidth = 100;
        GridHeight = 100;
        TilesPerUnit = 1;

        RoomList = new List<Room>();

        grid = GetComponent<TileGrid>();

        RNG = new System.Random(RNGSeed);

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Creates grid.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Creating Grid...");
            grid.SetGridSize(GridHeight, GridWidth, TilesPerUnit);
            grid.CreateGrid();
        }

        // Creates Rooms on grid
        if (Input.GetKeyDown(KeyCode.X) && (grid.TileArray.Length != 0))
        {
            Debug.Log("Creating Rooms...");
            createRandomRooms();
        }

        // Connect rooms using corridors
        if (Input.GetKeyDown(KeyCode.C) && (RoomList.Count > 0))
        {
            Debug.Log("Creating Corridors...");
            ConnectRooms();
        }

        // Cleanup any overlaping rooms and corridors.
        if (Input.GetKeyDown(KeyCode.V) && (RoomList.Count > 0))
        {
            Debug.Log("Cleaning up Rooms...");
            CleanUpRooms();
        }

    } // Update

    // Creates a Room given its Bottom left tile and the grid information.
    Room createRoom (int BotLeftTile, int width, int height)
    {
        if (width < 3)
        {
            Debug.Log("Minimum room width is 3");
            width = 3;
        }

        if (height < 3)
        {
            Debug.Log("Minimum room height is 3");
            height = 3;
        }

        int UpRightTile = 0;

        // Check input data against the grid.
        if (((BotLeftTile + width*grid.TilesPerWidth)%grid.TilesPerWidth) +1 >grid.TilesPerWidth)
        {
            // Room is too wide. Reduce width
            width = grid.TilesPerWidth + 1 - BotLeftTile;
        }

        // Calculate UpRightTile
        UpRightTile = BotLeftTile + width + (height * grid.TilesPerHeight);

        // Check if Up right tile is still inside tile array.
        if (UpRightTile > grid.TileArray.Length-1 )
        {
            // Overshoot, set UpRightTile as last tile on array.
            UpRightTile = grid.TileArray.Length - 1;
        }

        // Creates the room with the given data and add to the list.
        Room retRoom = new Room(BotLeftTile, UpRightTile, grid.TilesPerWidth);

        // Creates room on grid.
        grid.CreateRoomFromIndex(BotLeftTile, UpRightTile);

        return retRoom;
    }

    // Creates a random amount of rooms on the grid. Amount of numbers defined by Total Rooms
    void createRandomRooms ()
    {
        // Limits size of the rooms.
        int RoomsPerWidth = GridWidth *3/ TotalRooms;
        int RoomsPerHeight = GridHeight *3/ TotalRooms;

        // The UpperRight most room is a 3X3 room where the botleft tile is two colluns to the left and two lines down.
        int MaxUpRightTile = (grid.TileArray.Length + 1) - 2 - (2 * grid.TilesPerWidth);

        // Create rooms of varying sizes on the grid and add them to the room list.
        for (int i = 0; i < TotalRooms; i++)
        {
            RoomList.Add(createRoom(RNG.Next(MaxUpRightTile), RNG.Next(3, RoomsPerWidth), RNG.Next(3, RoomsPerHeight)));
        }
    }
    
    // Create corridors between created rooms.
    void ConnectRooms()
    {
        bool mustConnectRooms = false;
        int RoomCount = 0;
        foreach (Room curRoom in RoomList)
        {
            int CorridorCounter = 0;
            for (int i = RoomCount; i < RoomList.Count; i++)
            {
                // Switch between the different criteria to connect rooms
                switch (CorridorType)
                {
                    case CorridorConnect.AllToAll:

                        if (curRoom.CenterTile != RoomList[i].CenterTile)
                            mustConnectRooms = true;
                        else
                            mustConnectRooms = false;
                        break;

                    case CorridorConnect.Max1PerRoom:

                        if (CorridorCounter <= 1)
                        {
                            mustConnectRooms = true;
                            CorridorCounter++;
                        }
                        else
                            mustConnectRooms = false;
                        break;

                    case CorridorConnect.Max2PerRoom:

                        if (CorridorCounter <= 2)
                        {
                            mustConnectRooms = true;
                            CorridorCounter++;
                        }
                        else
                            mustConnectRooms = false;
                        break;

                    case CorridorConnect.Max3PerRoom:

                        if (CorridorCounter <= 3)
                        {
                            mustConnectRooms = true;
                            CorridorCounter++;
                        }
                        else
                            mustConnectRooms = false;
                        break;

                    default: // Connect All to All
                        if (curRoom.CenterTile == RoomList[i].CenterTile)
                            mustConnectRooms = false;
                        else
                            mustConnectRooms = true;
                        break;
                }// switch

                // Connect rooms acording to the criteria
                if (mustConnectRooms)
                {
                    grid.CreateCorridorFromIndex(curRoom.CenterTile, RoomList[i].CenterTile);
                } //if
            }//for
            RoomCount++;
        }//foreach
    }

    // Iterate through rooms changing tiles and making them prety..
    void CleanUpRooms()
    {
        int StartTile = 0;
        int EndTile = 0;
        foreach (Room curRoom in RoomList)
        {
            StartTile = curRoom.BotLeftTile; //+ 1 + grid.TilesPerWidth;
            EndTile = curRoom.UpRightTile; //- 1 - grid.TilesPerWidth;

            for (int i = StartTile; i < EndTile; i++)
            {
                #region Room Interior
                // Select Tiles Rows (Division)
                if (((StartTile/grid.TilesPerWidth < (i / grid.TilesPerWidth)) && ((i / grid.TilesPerWidth) < EndTile/ grid.TilesPerWidth)) &&
                    // Select Tiles Columns (Module)
                    (StartTile % grid.TilesPerWidth < (i % grid.TilesPerWidth)) && ((i % grid.TilesPerWidth) < EndTile % grid.TilesPerWidth))
                {
                    grid.TileArray[i].GetComponent<Tile>().SetTileType(TileType.Room);
                }
                #endregion
            }
        }
    }
}
