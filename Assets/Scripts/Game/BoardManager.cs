using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Transform boardTransf;

    public GameObject floorRef;
    public GameObject wallRef;
    public GameObject doorRef;

    public GameObject enemyRef;
    public GameObject cardPickupRef;

    public Sprite[] floorTiles;
    public Sprite[] wallTiles;
    public Sprite doorTile;

    public Dictionary<Coordinate, Board> boardMap;
    public Board currentBoard;

    private void Start()
    {
        if(boardMap == null)
            boardMap = new Dictionary<Coordinate, Board>();
    }

    public Vector3 GetRandomLocation()
    {
        return currentBoard.GetRandomLocation();
    }

    public void GenerateStartingBoard()
    {
        if(boardMap == null)
            boardMap = new Dictionary<Coordinate, Board>();

        Coordinate coord = new Coordinate(0, 0);

        currentBoard = new Board(coord, wallTiles.Length).GenerateStartingBoard();
        boardMap.Add(coord, currentBoard);

        DrawBoard();
        GenerateCards(1, 1);
    }

    public void SwitchBoard(Coordinate coord)
    {
        if(currentBoard != null)
        {
            EraseBoard();
        }

        if(boardMap.ContainsKey(coord))
        {
            currentBoard = (Board) boardMap[coord];
            PlaceEntities();
        }
        else
        {
            currentBoard = GenerateBoard(coord);
            GenerateEntities();
        }

        DrawBoard();
    }

    public Board GenerateBoard(Coordinate coord)
    {
        Board newBoard = new Board(coord, wallTiles.Length);
        boardMap.Add(coord, newBoard);
        GameManager.singleton.BoardGenerated();

        return newBoard;
    }

    public void EraseBoard()
    {
        for(int i = boardTransf.childCount - 1; i >= 0; i--)
        {
            Destroy(boardTransf.GetChild(i).gameObject);
        }
    }

    public void DrawBoard()
    {
        Board board = currentBoard;
        int tIndex = board.tilesetIndex;

        // Place Tiles
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

        // Place entities
        // foreach currentboard.entityData place entity

        Camera.main.transform.position = new Vector3(currentBoard.Width() / 2, 3.2f, -10);
    }

    // Called the first time a room is visited
    public void GenerateEntities()
    {
        GenerateCards();
        GenerateEnemies();
    }

    public void GenerateCards(int min = 1, int max = 2)
    {
        // 2-3 cards
        int numEntities = Random.Range(min, max + 1);
        for(int i = 0; i < numEntities; i++)
        {
            GameManager gm = GameManager.singleton;
            CardPickup card = Instantiate(cardPickupRef, GetRandomLocation(), Quaternion.identity).GetComponent<CardPickup>();
            card.SetCard((CardInfo) gm.cardDatabase.GetRandomItemOfLevel(gm.killCounter));
            GameManager.singleton.RegisterEntity(card);
        }
    }

    public void GenerateEnemies()
    {
        // 1-3 enemies
        int numEntities = Random.Range(1, 4);
        for(int i = 0; i < numEntities; i++)
        {
            Enemy enemy = Instantiate(enemyRef, GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>();
            GameManager.singleton.RegisterEntity(enemy);
        }
    }

    // Called after visiting a room after initially generating
    public void PlaceEntities()
    {
        GameManager.singleton.entities = new ArrayList();

        foreach(Entity.Data entData in currentBoard.entityData.Values)
        {
            Entity newEnt = null;

            if(entData.type == Entity.Type.Card)
            {
                newEnt = Instantiate(cardPickupRef).GetComponent<Entity>();
            }
            else if(entData.type == Entity.Type.Enemy)
            {
                newEnt = Instantiate(enemyRef).GetComponent<Entity>();
            }

            if(newEnt)
            {
                // Set data places them in the correct position
                newEnt.SetData(entData);
                GameManager.singleton.RegisterEntity(newEnt);
            }
        }
    }
}
