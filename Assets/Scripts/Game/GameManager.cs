using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager singleton;
    [HideInInspector]
    public BoardManager boardManager;

    public UIManager uiManager;

    public Database cardDatabase;
    public Database enemyDatabase;

    public int boardCounter; // Incremented every board generation, used for generating higher level monsters
    public int killCounter; // Incremented every monster kill, used for generating higher level cards

    public Vector2 enemyTierDelayRange;
    public Vector2 itemTierDelayRange;

    public int enemyTierDelay;
    public int itemTierDelay;

    public int enemyTier = 0;
    public int itemTier = 0;

    public GameObject playerRef;
    //public SortedList<int, List<GameObject>> enemyLists;

    public GameObject playerUI;

    public bool somethingMoving = false;

    public bool isPlayerTurn;
    public int entityTurn;

    public Player player;
    public ArrayList entities;

    [HideInInspector]
    public ContactFilter2D defaultCF;

    [HideInInspector]
    public bool isPaused = false;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }

        boardManager = GetComponent<BoardManager>();
    }

    private void Start()
    {
        itemTierDelay = (int) itemTierDelayRange.x;
        enemyTierDelay = (int) enemyTierDelayRange.y;

        defaultCF.SetLayerMask(LayerMask.NameToLayer("Default"));

        InitLevel();
    }

    private void Update()
    {
        if(isPaused) return;
        if(somethingMoving) return;

        if(!isPlayerTurn)
        {
            if(entityTurn != entities.Count)
            {
                if(((Entity) entities[entityTurn]).data.type == Entity.Type.Enemy)
                {
                    Enemy enemy = (Enemy) entities[entityTurn];
                    if(enemy != null)
                    {
                        enemy.DoTurn();
                    }
                    else
                    {
                        entities.RemoveAt(entityTurn);
                    }
                }
                else
                {
                    entityTurn++;
                }
            }
            else
            {
                isPlayerTurn = true;
            }
            
        }
    }

    // Init first level
    public void InitLevel()
    {
        entities = new ArrayList();
        boardManager.GenerateStartingBoard();
        GameObject playerObj = Instantiate(playerRef, new Vector3(boardManager.currentBoard.Width() / 2, boardManager.currentBoard.Height() / 2, 0), Quaternion.identity);

        player = playerObj.GetComponent<Player>();
        isPlayerTurn = true;
    }

    public void ChangeBoard(Vector2 direction)
    {
        Coordinate curCoord = boardManager.currentBoard.coord;
        Coordinate newCoord = new Coordinate(curCoord.x + (int) direction.x, curCoord.y + (int) direction.y);

        // Delete all entities
        foreach(Entity ent in entities)
        {
            Destroy(ent.gameObject);
        }
        entities = new ArrayList();

        // Load new board
        boardManager.SwitchBoard(newCoord);

        // Put player in correct position
        if(direction.x == 0)
        {
            if(direction.y == 1)
                player.transform.position = new Vector3(10, 1);
            else
                player.transform.position = new Vector3(10, 9);
        }
        else
        {
            if(direction.x == 1)
                player.transform.position = new Vector3(1, 5);
            else
                player.transform.position = new Vector3(18, 5);
        }
    }

    // Called when board manager generates a new room
    public void BoardGenerated()
    {
        boardCounter++;

        if(boardCounter >= enemyTierDelay)
        {
            enemyTierDelay += Mathf.RoundToInt(Random.Range(enemyTierDelayRange.x, enemyTierDelayRange.y));
            enemyTier++;
        }

        player.Reshuffle();
    }

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        entityTurn = 0;
    }

    public void EndEnemyTurn()
    {
        entityTurn++;
    }

    public void RegisterEntity(Entity entity)
    {
        if(!entities.Contains(entity))
        {
            entities.Add(entity);
            boardManager.currentBoard.RegisterEntity(entity);
        }
    }

    public void UnregisterEntity(Entity entity)
    {
        if(entities.Contains(entity))
        {
            entities.Remove(entity);
            boardManager.currentBoard.UnregisterEntity(entity);
        }
    }

    public void EnableCardColliders()
    {
        foreach(Entity ent in entities)
        {
            if(ent.tag == "Card")
            {
                ent.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void DisableCardColliders()
    {
        foreach(Entity ent in entities)
        {
            if(ent.tag == "Card")
            {
                ent.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void EnemyKilled(Enemy enemy)
    {
        killCounter++;

        if(killCounter >= itemTierDelay)
        {
            itemTierDelay += Mathf.RoundToInt(Random.Range(itemTierDelayRange.x, itemTierDelayRange.y));
            itemTier++;
        }

        player.score += enemy.info.scoreValue;
    }

    public void PlayerKilled()
    {
        uiManager.ShowGameOverUI();
        Destroy(player.gameObject);
        player = null;
    }

    public void Pause()
    {
        isPaused = true;
        uiManager.ShowPauseUI();
    }

    public void UnPause()
    {
        isPaused = false;
        uiManager.HidePauseUI();
    }
}
