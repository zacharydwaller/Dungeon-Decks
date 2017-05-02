using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager singleton;
    [HideInInspector]
    public BoardManager boardManager;

    public int boardCounter; // Incremented every board generation, used for generating higher level monsters
    public int killCounter; // Incremented every monster kill, used for generating higher level cards

    public GameObject playerRef;
    //public SortedList<int, List<GameObject>> enemyLists;
    public GameObject enemyRef;

    public GameObject playerUI;

    public bool somethingMoving = false;

    public bool isPlayerTurn;
    public int enemyTurn;

    public Player player;
    public ArrayList enemies;

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

        DontDestroyOnLoad(gameObject);

        boardManager = GetComponent<BoardManager>();
    }

    private void Start()
    {
        InitLevel();
    }

    private void Update()
    {
        if(somethingMoving) return;

        if(!isPlayerTurn)
        {
            if(enemyTurn != enemies.Count)
            {
                Enemy enemy = (Enemy) enemies[enemyTurn];
                if(enemy != null)
                {
                    enemy.DoTurn();
                }
                else
                {
                    enemies.RemoveAt(enemyTurn);
                }
            }
            else
            {
                isPlayerTurn = true;
            }
            
        }
    }

    public void InitLevel()
    {
        boardManager.LoadStartingBoard();
        GameObject playerObj = Instantiate(playerRef, new Vector3(boardManager.currentBoard.Width() / 2, boardManager.currentBoard.Height() / 2, 0), Quaternion.identity);
        player = playerObj.GetComponent<Player>();

        enemies = new ArrayList();
        for(int i = 0; i < 2; i++)
        {
            Enemy enemy = Instantiate(enemyRef, boardManager.GetRandomLocation(), Quaternion.identity).GetComponent<Enemy>();
            enemies.Add(enemy);
            boardManager.RegisterEntity(enemy);
        }

        isPlayerTurn = true;
    }

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        enemyTurn = 0;
    }

    public void EndEnemyTurn()
    {
        enemyTurn++;
    }

    public void EnemyKilled()
    {

    }

    public void PlayerKilled()
    {

    }
}
