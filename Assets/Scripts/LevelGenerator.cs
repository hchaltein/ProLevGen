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
    Room[] RoomList;

	// Use this for initialization
	void Awake()
    {
        Grid = GetComponent<TileGrid>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid.SetGridSize(10, 10, 2);
            Grid.CreateGrid();
        }

        if (Input.GetKeyDown(KeyCode.Z) && (Grid.TileArray.Length != 0))
        {
            Grid.CreateRoomFromTileIndex(45, 348);
        }
	}
}
