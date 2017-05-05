using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/HealthSteal")]
public class HealthSteal : CardEffect
{
    public int damage;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2))
    {
        Collider2D collider = user.GetComponent<Collider2D>();
        RaycastHit2D rayHit;
        Vector2 start = user.transform.position;
        Vector2 dest = start + direction;

        collider.enabled = false;
        rayHit = Physics2D.Linecast(start, dest);
        collider.enabled = true;

        if(rayHit.transform != null)
        {
            Entity ent = rayHit.transform.GetComponent<Entity>();

            if(ent)
            {
                Entity userEnt = user.GetComponent<Entity>();
                userEnt.DoAttackAnimation(direction);
                userEnt.health += damage;
                ent.TakeDamage(damage);
            }
        }
    }

    public override int GetEffectAmount()
    {
        return damage;
    }
}
