using UnityEngine;
using System.Collections;

// Requirements
[RequireComponent(typeof(TileGrid))]

public class LevelGenerator : MonoBehaviour {

    // Grid Varialbes
    TileGrid Grid;
    public int GridHeight = 10;
    public int GridWidth = 10;
    public int TilesPerUnit = 1;

    public int RoomNumber = 1;
    Room Room1;
    Room Room2;
    Room Room3;
    Room Room4;

    public int Room1Nmbr, Room2Nmbr, Room3Nmbr, Room4Nmbr;


    // Use this for initialization
    void Awake()
    {
        Grid = GetComponent<TileGrid>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Grid.SetGridSize(20, 20, 2);
            Grid.CreateGrid();
        }

        if (Input.GetKeyDown(KeyCode.X) && (Grid.TileArray.Length != 0))
        {
            Grid.CreateRoomFromIndex(45, 455);
            Grid.CreateRoomFromIndex(522, 686);
            Grid.CreateRoomFromIndex(538, 703);
            Grid.CreateRoomFromIndex(728, 852);
            Room1 = new Room(45, 455, Grid.TilesPerWidth);
            Room2 = new Room(522, 686, Grid.TilesPerWidth);
            Room3 = new Room(538, 703, Grid.TilesPerWidth);
            Room4 = new Room(728, 852, Grid.TilesPerWidth);

            Room1Nmbr = Room1.CenterTile;
            Room2Nmbr = Room2.CenterTile;
            Room3Nmbr = Room3.CenterTile;
            Room4Nmbr = Room3.CenterTile;
        }

        if (Input.GetKeyDown(KeyCode.C) && (Grid.TileArray.Length != 0))
        {
            Grid.CreateCorridorFromIndex(Room2.CenterTile, Room1.CenterTile);
            Grid.CreateCorridorFromIndex(Room1.CenterTile, Room3.CenterTile);
            Grid.CreateCorridorFromIndex(Room2.CenterTile, Room3.CenterTile);
            Grid.CreateCorridorFromIndex(Room1.CenterTile, Room4.CenterTile);
        }

        
    }
}
