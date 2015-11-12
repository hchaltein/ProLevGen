using UnityEngine;
using System.Collections;

public class Room
{

    public int Width, Height;
    public int TileWidth, TileHeight;
    public int TilesPerUnit;

    public int RoomCenter;

    public int BotLeftTile, BotRightTile, UpLeftTile, UpRightTile;

    public int[] TileIndexList;

    // Contructors
    // Default
    Room(int _TilesPerUnit, int _Width = 2 , int _Height = 2)
    {
        // Get Values
        Width = _Width;
        Height = _Height;
        TilesPerUnit = _TilesPerUnit;
        // Calcule Room size in Tiles
        TileWidth = Width * TilesPerUnit;
        TileHeight = Height * TilesPerUnit;

        TileIndexList = new int[TileWidth * TileHeight];
    }

}
