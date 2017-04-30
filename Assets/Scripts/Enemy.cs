using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    int damage;

    protected override void Start()
    {
        base.Start();

        health = 3;
        damage = 1;
    }

    public override void DoTurn()
    {
        Vector3 target = GameManager.singleton.player.transform.position;
        Vector2 dir = target - transform.position;
        dir.Normalize();

        dir.x = Mathf.Round(dir.x);
        dir.y = Mathf.Round(dir.y);

        RaycastHit2D rayHit;
        if(!Move(dir, out rayHit))
        {
            // Hit something
            if(rayHit.transform.tag == "Player")
            {
                Attack(damage, dir, GameManager.singleton.player);
            }
        }

        GameManager.singleton.EndEnemyTurn();
    }

    protected override void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            GameManager.singleton.killCounter++;
            Destroy(gameObject);
        }
    }
}
