using UnityEngine;
using System.Collections;

// Tiles that will be filled to create the level.
public enum TileType
{
    Unassigned,
    Wall,
    WallBorder,
    Room,
    Corridor
}

public class Tile : MonoBehaviour
{
    public TileType Type;

    // Use this for initialization
    void Start()
    {
        SetProperMaterial();
    }

    // Select and Assign the Correct Material to a Tile Given its
    void SetProperMaterial()
    {
        Material Material;
    
        // Load Correct Material
        switch (Type)
        {
            case TileType.Unassigned:
                Material = Resources.Load<Material>("Unassigned");
                break;

            case TileType.Wall:
                Material = Resources.Load<Material>("Wall");
                break;

            case TileType.WallBorder:
                Material = Resources.Load<Material>("WallBorder");
                break;

            case TileType.Room:
                Material = Resources.Load<Material>("Room");
                break;

            case TileType.Corridor:
                Material = Resources.Load<Material>("Corridor");
                break;

            default:
                Material = Resources.Load<Material>("Room");
                break;
        }

        // Set Material
        GetComponent<MeshRenderer>().material = Material;
    }
}
