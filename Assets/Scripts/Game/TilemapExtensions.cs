using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions
{
    // Better BoxFill than default Tilemap BoxFill
    public static void BoxFill(this Tilemap map, TileBase tile, Vector3Int start, Vector3Int end)
    {
        var xDir = start.x < end.x ? 1 : -1;
        var yDir = start.y < end.y ? 1 : -1;

        int xCols = Mathf.Abs(start.x - end.x);
        int yCols = Mathf.Abs(start.y - end.y);

        for (var x = 0; x < xCols; x++)
        {
            for (var y = 0; y < yCols; y++)
            {
                var tilePos = start + new Vector3Int(x * xDir, y * yDir, 0);
                map.SetTile(tilePos, tile);
            }
        }
    }

    public static void BoxFill(this Tilemap map, TileBase tile, Vector3 start, Vector3 end)
    {
        BoxFill(map, tile, map.WorldToCell(start), map.WorldToCell(end));
    }

    public static void DrawLine(this Tilemap map, TileBase tile, Vector3Int start, Vector3Int end)
    {
        var xDir = start.x < end.x ? 1 : -1;
        var yDir = start.y < end.y ? 1 : -1;

        int xCols = Mathf.Abs(start.x - end.x);
        int yCols = Mathf.Abs(start.y - end.y);


    }
}
