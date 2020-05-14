using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom
{
    public int Id;
    public int Level;
    public Vector2Int Coordinate;
    public Dictionary<Direction, int> Connections;

    public int Width;
    public int Height;

    public bool FullyConnected { get => Connections.Count == DirectionUtility.NumDirections(); }

    public DungeonRoom(int id, int level, Vector2Int coord, int width, int height)
    {
        Id = id;
        Level = level;
        Coordinate = coord;
        Connections = new Dictionary<Direction, int>();

        Width = width;
        Height = height;
    }

    public void DrawRoom()
    {
        //var dm = GameManager.singleton.DungeonManager;
        //var wallPrefab = dm.WallPrefab;
        //var wallTile = dm.WallSprites[(Level - 1) % dm.WallSprites.Length];
        //var floorPrefab = dm.FloorPrefab;
        //var floorTile = dm.FloorSprites[(Level - 1) % dm.FloorSprites.Length];
        //var doorPrefab = dm.FloorPrefab;
        //var doorSprite = dm.DoorSprite;

        //// Place Tiles
        //for (int row = 0; row < Height; row++)
        //{
        //    for (int col = 0; col < Width; col++)
        //    {
        //        // Place Tile
        //        GameObject tileRef = floorPrefab;
        //        Sprite spriteRef = floorTile;

        //        //switch (GetTile(row, col))
        //        //{
        //        //    case TileType.Floor:
        //        //        tileRef = floorPrefab;
        //        //        spriteRef = floorTile;
        //        //        break;
        //        //    case TileType.Door:
        //        //        tileRef = doorPrefab;
        //        //        spriteRef = doorSprite;
        //        //        break;
        //        //}

        //        GameObject tileObj = GameObject.Instantiate(tileRef, dm.BoardTransform);
        //        tileObj.transform.localPosition = new Vector3(col, row, 0);
        //        tileObj.GetComponentInChildren<SpriteRenderer>().sprite = spriteRef;
        //    }
        //}

        // Place entities
        // foreach currentboard.entityData place entity

        //Camera.main.transform.position = new Vector3(currentBoard.Width() / 2, 3.2f, -10);
    }

    #region DungeonGeneration

    public void AddConnection(Direction dir, int roomId)
    {
        try
        {
            Connections.Add(dir, roomId);
        }
        catch (Exception /*e*/) { }
    }

    #endregion
}
