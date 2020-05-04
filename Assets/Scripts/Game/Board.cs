using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Floor, Wall, Door
}

public class Board
{
    public int tilesetIndex;

    private int width;
    private int height;

    public Coordinate coord;

    private TileType[] tiles;

    // id, entityData
    public SortedList<int, Entity.Data> entityData;

    public Board(Coordinate newCoord, int numTilesets)
    {
        coord = newCoord;
        tilesetIndex = Random.Range(0, numTilesets);

        width = 15;
        height = 11;

        tiles = new TileType[width * height];
        entityData = new SortedList<int, Entity.Data>();

        SetRoomTiles();
    }

    public void RegisterEntity(Entity entity)
    {
        if(!entityData.ContainsKey(entity.data.id))
        {
            entityData.Add(entity.data.id, entity.data);
        }
    }

    public void UnregisterEntity(Entity entity)
    {
        if(entityData.ContainsKey(entity.data.id))
        {
            entityData.Remove(entity.data.id);
        }
    }

    public Vector3 GetRandomLocation(int padding = 0)
    {
        Vector3 ret = new Vector3();

        do
        {
            ret.x = Random.Range(0 + padding, width - padding);
            ret.y = Random.Range(0 + padding, height - padding);
        } while(GetTile((int) ret.y, (int) ret.x) != TileType.Floor);

        return ret;
    }

    public Board GenerateStartingBoard()
    {
        return this;
    }

    private void SetRoomTiles()
    {
        // Top/Bottom wall
        for(int col = 0; col < width; col++)
        {
            SetTile(0, col, TileType.Wall);
            SetTile(height - 1, col, TileType.Wall);

            if(col == width / 2)
            {
                SetTile(0, col, TileType.Door);
                SetTile(height - 1, col, TileType.Door);
            }
        }

        // Left/Right wall
        for(int row = 0; row < height; row++)
        {
            SetTile(row, 0, TileType.Wall);
            SetTile(row, width - 1, TileType.Wall);

            if(row == height / 2)
            {
                SetTile(row, 0, TileType.Door);
                SetTile(row, width - 1, TileType.Door);
            }
        }
    }

    public TileType SetTile(int row, int col, TileType newValue)
    {
        if(row < height && col < width)
        {
            tiles[(row * width) + col] = newValue;
            return newValue;
        }
        else
        {
            return TileType.Wall;
        }
    }

    public TileType GetTile(int row, int col)
    {
        if(row < height && col < width)
        {
            return tiles[(row * width) + col];
        }
        else
        {
            return TileType.Wall;
        }
    }

    public int Width() { return width; }
    public int Height() { return height; }
}

public struct Coordinate
{
    public int x;
    public int y;

    public Coordinate(int x_, int y_)
    {
        x = x_;
        y = y_;
    }

    public Coordinate Add(int xAdd, int yAdd)
    {
        x += xAdd;
        y += yAdd;

        return this;
    }
}