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

    private CardInfo prevCard;

    private void Start()
    {
        if(boardMap == null)
            boardMap = new Dictionary<Coordinate, Board>();

        prevCard = null;
    }

    public Vector3 GetRandomLocation(int padding, List<Vector3> prevLocations)
    {
        Vector3 location;

        do
        {
            location = currentBoard.GetRandomLocation(padding);
        } while (prevLocations.Contains(location));

        return location;
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
                    case TileType.Floor:
                        tileRef = floorRef;
                        spriteRef = floorTiles[tIndex];
                        break;
                    case TileType.Wall:
                        tileRef = wallRef;
                        spriteRef = wallTiles[tIndex];
                        break;
                    case TileType.Door:
                        tileRef = doorRef;
                        spriteRef = doorTile;
                        break;
                }

                GameObject tileObj = GameObject.Instantiate(tileRef, boardTransf);
                tileObj.transform.localPosition = new Vector3(col, row, 0);
                tileObj.GetComponentInChildren<SpriteRenderer>().sprite = spriteRef;
            }
        }

        // Place entities
        // foreach currentboard.entityData place entity

        //Camera.main.transform.position = new Vector3(currentBoard.Width() / 2, 3.2f, -10);
    }

    // Called the first time a room is visited
    public void GenerateEntities()
    {
        GenerateCards();
        GenerateEnemies();
    }

    public void GenerateCards(int min = 1, int max = 3)
    {
        // 1-3 cards
        int numEntities = Random.Range(min, max + 1);
        GameManager gm = GameManager.singleton;
        Player player = gm.player;

        List<CardInfo> prevCards = new List<CardInfo>();
        prevCards.Add(prevCard);

        var prevLocations = new List<Vector3>();

        for(int i = 0; i < numEntities; i++)
        {
            var location = GetRandomLocation(2, prevLocations);
            prevLocations.Add(location);

            CardPickup card = Instantiate(cardPickupRef, location, Quaternion.identity).GetComponent<CardPickup>();
            CardInfo newCard;

            int maxIterations = max * 2;
            int iteration = 0;
            do
            {
                newCard = CardDatabase.GetCardOfStatTier(player.primaryStats[0], player.primaryStats[1], gm.cardTier);
                iteration++;
            } while(prevCards.Contains(newCard) && iteration < maxIterations);

            prevCards.Add(newCard);
            prevCard = newCard;

            card.SetCard(newCard);

            GameManager.singleton.RegisterEntity(card);
        }
    }

    public void GenerateEnemies(int min = 2, int max = 6)
    {
        GameManager gm = GameManager.singleton;

        // 2-6 enemies - unless facing the singular Dragon
        int numEntities = Random.Range(min, max + 1);
        if (gm.enemyTier == GameManager.bossTier)
        {
            numEntities = 1;
        }

        var prevLocations = new List<Vector3>();

        for(int i = 0; i < numEntities; i++)
        {
            var location = GetRandomLocation(3, prevLocations);
            prevLocations.Add(location);

            var enemyInfo = EnemyDatabase.GetEnemyOfTier(gm.enemyTier);

            if(enemyInfo != null)
            {
                Enemy enemy = Instantiate(enemyRef, location, Quaternion.identity).GetComponent<Enemy>();
                enemy.SetEnemy(enemyInfo);
                GameManager.singleton.RegisterEntity(enemy);
            }
            else
            {
                Debug.Log($"Enemy info null. Tier {gm.enemyTier}");
            }
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
