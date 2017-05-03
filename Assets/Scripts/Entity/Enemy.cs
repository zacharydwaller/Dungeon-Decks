using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int damage;
    public int scoreValue;

    protected override void Awake()
    {
        base.Awake();

        data.type = Entity.Type.Enemy;
    }

    protected override void Start()
    {
        base.Start();

        health = 1;
        damage = 1;
        scoreValue = 100;
    }

    public override void DoTurn()
    {
        Vector3 target = GameManager.singleton.player.transform.position;
        Vector2 dir = target - transform.position;
        dir.Normalize();

        // Ensure diagonals are not possible but also pick most appropriate direction
        if(Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            dir.y = 0;
            dir.x = Mathf.Round(dir.x);
        }
        else
        {
            dir.x = 0;
            dir.y = Mathf.Round(dir.y);
        }

        RaycastHit2D rayHit;
        if(CheckMove(dir, out rayHit))
        {
            Move(dir);
        }
        else
        {
            // Hit player
            if(rayHit.transform.tag == "Player")
            {
                Attack(damage, dir, GameManager.singleton.player);
            }
            // Hit card, move anyways
            else if(rayHit.transform.tag == "Card")
            {
                Move(dir);
            }
        }

        GameManager.singleton.EndEnemyTurn();
    }

    protected override void TakeDamage(int amount)
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

    public override void SetData(Data newData)
    {
        base.SetData(newData);

        health = data.health;
    }
}
