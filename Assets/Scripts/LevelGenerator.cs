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
        if (Input.GetKey(KeyCode.Space))
        {
            Grid.SetGridSize(20, 20, 4);
            Grid.CreateGrid();
        }
	}
}
