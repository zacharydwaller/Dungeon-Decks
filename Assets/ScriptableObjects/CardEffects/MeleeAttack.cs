using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/MeleeAttack")]
public class MeleeAttack : CardEffect
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
                user.GetComponent<Entity>().DoAttackAnimation(direction);
                ent.TakeDamage(damage);
            }
        }
    }

    public override int GetEffectAmount()
    {
        return damage;
    }
}
