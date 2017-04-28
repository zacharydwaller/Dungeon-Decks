using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform boardTransf;

    public GameObject floorRef;
    public GameObject wallRef;
    public GameObject doorRef;

    public Sprite[] floorTiles;
    public Sprite[] wallTiles;
    public Sprite doorTile;

    public Hashtable boardMap;
    public Board currentBoard;

    private void Start()
    {
        if(boardMap == null)
            boardMap = new Hashtable();
    }

    public void RegisterEntity(Entity entity)
    {
        currentBoard.entityData.Add(entity.data);
    }

    public Vector3 GetRandomLocation()
    {
        return currentBoard.GetRandomLocation();
    }

    public void LoadStartingBoard()
    {
        if(boardMap == null)
            boardMap = new Hashtable();

        currentBoard = new Board(wallTiles.Length).GenerateStartingBoard();
        boardMap.Add(new Coordinate(0,0).GetHashCode(), currentBoard);

        SwitchBoard(new Coordinate(0, 0));
    }

    public void SwitchBoard(Coordinate coord)
    {
        if(currentBoard != null)
        {
            EraseBoard();
        }

        if(boardMap.ContainsKey(coord.GetHashCode()))
        {
            currentBoard = (Board) boardMap[coord.GetHashCode()];
        }
        else
        {
            currentBoard = new Board(wallTiles.Length);
            boardMap.Add(coord.GetHashCode(), currentBoard);
        }

        DrawBoard();
    }

    public void EraseBoard()
    {
        for(int i = boardTransf.childCount - 1; i >= 0; i--)
        {
            Destroy(boardTransf.GetChild(i));
        }
    }

    public void DrawBoard()
    {
        Board board = currentBoard;
        int tIndex = board.tilesetIndex;

        for(int row = 0; row < board.Height(); row++)
        {
            for(int col = 0; col < board.Width(); col++)
            {
                // Place Tile
                GameObject tileRef = wallRef;
                Sprite spriteRef = wallTiles[tIndex];

                switch(board.GetTile(row, col))
                {
                    case Tile.Floor:
                        tileRef = floorRef;
                        spriteRef = floorTiles[tIndex];
                        break;
                    case Tile.Wall:
                        tileRef = wallRef;
                        spriteRef = wallTiles[tIndex];
                        break;
                    case Tile.Door:
                        tileRef = doorRef;
                        spriteRef = doorTile;
                        break;
                }

                GameObject tileObj = GameObject.Instantiate(tileRef, boardTransf);
                tileObj.transform.localPosition = new Vector3(col, row, 0);
                tileObj.GetComponent<SpriteRenderer>().sprite = spriteRef;
            }
        }

        Camera.main.transform.position = new Vector3(currentBoard.Width() / 2, currentBoard.Height() / 2, -10);
    }
}

public enum Tile
{
    Floor, Wall, Door
}

public class Coordinate
{
    int x;
    int y;

    int row;
    int col;

    public Coordinate(int x_, int y_)
    {
        x = col = x_;
        y = row = y_;
    }
}

public class Board
{
    public int tilesetIndex;

    private int width;
    private int height;

    private Tile[] tiles;

    public List<Entity.Data> entityData;

    public Board(int numTilesets)
    {
        tilesetIndex = Random.Range(0, numTilesets);

        width = 20;
        height = 11;

        tiles = new Tile[width * height];
        entityData = new List<Entity.Data>();

        SetRoomTiles();
    }

    public Vector3 GetRandomLocation()
    {
        Vector3 ret = new Vector3();

        do
        {
            ret.x = Random.Range(0, width);
            ret.y = Random.Range(0, height);
        } while(GetTile((int) ret.y, (int) ret.x) != Tile.Floor);

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
            SetTile(0, col, Tile.Wall);
            SetTile(height - 1, col, Tile.Wall);

            if(col == width / 2)
            {
                SetTile(0, col, Tile.Door);
                SetTile(height - 1, col, Tile.Door);
            }
        }

        // Left/Right wall
        for(int row = 0; row < height; row++)
        {
            SetTile(row, 0, Tile.Wall);
            SetTile(row, width - 1, Tile.Wall);

            if(row == height / 2)
            {
                SetTile(row, 0, Tile.Door);
                SetTile(row, width - 1, Tile.Door);
            }
        }
    }

    public Tile SetTile(int row, int col, Tile newValue)
    {
        if(row < height && col < width)
        {
            tiles[(row * width) + col] = newValue;
            return newValue;
        }
        else
        {
            return Tile.Wall;
        }
    }

    public Tile GetTile(int row, int col)
    {
        if(row < height && col < width)
        {
            return tiles[(row * width) + col];
        }
        else
        {
            return Tile.Wall;
        }
    }

    public int Width() { return width; }
    public int Height() { return height; }
}