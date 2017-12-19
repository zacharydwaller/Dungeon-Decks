using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TooltipCreator))]
public class Enemy : Entity
{
    public EnemyInfo info;

    bool takingTurn;
    TooltipCreator tooltipCreator;

    protected override void Awake()
    { 
        base.Awake();

        data.type = Entity.Type.Enemy;

        tooltipCreator = GetComponent<TooltipCreator>();
    }

    protected override void Start()
    {
        base.Start();

        health = info.maxHealth;
        maxAuras = 6;
        takingTurn = false;
    }

    public override void SetData(Data newData)
    {
        base.SetData(newData);

        health = data.health;

        info = (EnemyInfo) data.info;
        GetComponent<SpriteRenderer>().sprite = info.sprite;
        tooltipCreator.SetItem(this);
    }

    public void SetEnemy(EnemyInfo newInfo)
    {
        info = newInfo;
        data.info = info;
        GetComponent<SpriteRenderer>().sprite = info.sprite;
        health = info.maxHealth;
        tooltipCreator.SetItem(this);
    }

    public override void DoTurn()
    {
        takingTurn = true;

        TickAuras();
        if(health <= 0) return;

        if(!GameManager.singleton.player)
        {
            GameManager.singleton.EndEnemyTurn();
        }

        if(GameManager.singleton.player == null) return;

        RaycastHit2D rayHit;
        Vector3 target = GameManager.singleton.player.transform.position;
        Vector2 dir = target - transform.position;

        // Ensure diagonals are not possible but also pick most appropriate direction
        // Unless one direction is incredibly larger than other, move randomly
        if((dir.x != 0 && dir.y != 0) && Mathf.Abs((dir.x * dir.x) - (dir.y * dir.y)) < 10)
        {
            bool horz = false;

            if(Random.Range(0f, 1f) < 0.5f)
            {
                // Random checked, try to move horizontally
                if(CheckMove(new Vector2(dir.x, 0), out rayHit))
                {
                    horz = true;
                }
                // else horizontal blocked, move verticaly
            }
            else
            {
                // Random checked, try to move vertically
                if(CheckMove(new Vector2(0, dir.y), out rayHit))
                {
                    horz = false;
                }
                else
                {
                    // Vertical blocked, move horizontally
                    horz = true;
                }
            }

            if(horz)
            {
                dir.y = 0;
            }
            else
            {
                dir.x = 0;
            }
        }
        // Player further away in X direction
        else if(Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            dir.y = 0;
        }
        // Player further away in Y direction
        else
        {
            dir.x = 0;
        }

        dir.Normalize();
        
        if(CheckMove(dir, out rayHit))
        {
            Move(dir);
        }
        else
        {
            // Hit player
            if(rayHit.transform.tag == "Player")
            {
                DoAttackAnimation(dir);
                info.attackEffect.DoEffect(gameObject, dir, info.magnitude, info.secondary);
                rayHit.transform.GetComponent<Player>().AttackedByEnemy(this);
            }
        }

        GameManager.singleton.EndEnemyTurn();
        takingTurn = false;
    }

    public override void TakeDamage(float amount)
    {
        health -= amount;
        data.health = health;

        if(health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();

        GameManager.singleton.EnemyKilled(this);

        if(takingTurn)
        {
            takingTurn = false;
            GameManager.singleton.EndEnemyTurn();

            if(isMoving)
            {
                GameManager.singleton.somethingMoving = false;
            }
        }

        Destroy(gameObject);
    }
}
