using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TooltipCreator))]
public class Enemy : Entity
{
    public EnemyInfo info;

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
        if(!GameManager.singleton.player)
        {
            GameManager.singleton.EndEnemyTurn();
        }

        if(GameManager.singleton.player == null) return;

        RaycastHit2D rayHit;
        Vector3 target = GameManager.singleton.player.transform.position;
        Vector2 dir = target - transform.position;
        dir.Normalize();

        // Ensure diagonals are not possible but also pick most appropriate direction
        if(Mathf.Abs(dir.x) == Mathf.Abs(dir.y))
        {
            //Player at direct diagonal, check directions
            if(CheckMove(new Vector2(dir.x, 0), out rayHit))
            {
                dir.x = 1 * Mathf.Sign(dir.x);
                dir.y = 0;
            }
            else
            {
                dir.y = 1 * Mathf.Sign(dir.y);
                dir.x = 0;
            }
        }
        // Player further away in X direction
        else if(Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            dir.y = 0;
            dir.x = Mathf.Round(dir.x);
        }
        // Player further away in Y direction
        else
        {
            dir.x = 0;
            dir.y = Mathf.Round(dir.y);
        }

        
        if(CheckMove(dir, out rayHit))
        {
            Move(dir);
        }
        else
        {
            // Hit player
            if(rayHit.transform.tag == "Player")
            {
                info.attackEffect.DoEffect(gameObject, dir);
            }
            // Hit card, move anyways
            else if(rayHit.transform.tag == "Card")
            {
                Move(dir);
            }
        }

        GameManager.singleton.EndEnemyTurn();
    }

    public override void TakeDamage(int amount)
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
        Destroy(gameObject);
    }
}
