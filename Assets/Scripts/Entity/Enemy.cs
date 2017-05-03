using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int damage;
    public int scoreValue;

    protected override void Start()
    {
        base.Start();

        health = 3;
        damage = 2;
        scoreValue = 100;
    }

    public override void DoTurn()
    {
        Vector3 target = GameManager.singleton.player.transform.position;
        Vector2 dir = target - transform.position;
        dir.Normalize();

        dir.x = Mathf.Round(dir.x);
        dir.y = Mathf.Round(dir.y);

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

        if(health <= 0)
        {
            GameManager.singleton.EnemyKilled(this);
            Destroy(gameObject);
        }
    }
}
