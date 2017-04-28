using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

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
                Attack(dir, GameManager.singleton.player);
            }
        }

        GameManager.singleton.EndEnemyTurn();
    }
}
