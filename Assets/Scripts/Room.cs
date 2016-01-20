using UnityEngine;
using System.Collections;

public class Room
{
    // Public Variables
    public int BotLeftTile, UpRightTile;
    public int TilesPerWidth;

    // Private Variables
    public int CenterTile;


    // Contructors
    // Default
    public Room(int _BotLeftTile, int _UpRightTile, int _CenterTile, int _TilesPerWidth)
    {
        // Get Values
        BotLeftTile = _BotLeftTile;
        UpRightTile = _UpRightTile;
        CenterTile = _CenterTile;
        TilesPerWidth = _TilesPerWidth;
    }

    public Room(int _BotLeftTile, int _UpRightTile, int _TilesPerWidth)
    {
        // Get Values
        BotLeftTile = _BotLeftTile;
        UpRightTile = _UpRightTile;
        TilesPerWidth = _TilesPerWidth;
        
        //Calculate Center Tile
        int CenterTileX = ((UpRightTile - BotLeftTile) % TilesPerWidth)/2;
        int CenterTileY = ((UpRightTile - BotLeftTile) / TilesPerWidth)/2;

        CenterTile = BotLeftTile+ CenterTileX + (CenterTileY* TilesPerWidth);

    }

}
