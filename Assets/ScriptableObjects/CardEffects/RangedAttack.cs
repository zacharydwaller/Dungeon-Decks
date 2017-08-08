using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/RangedAttack")]
public class RangedAttack : CardEffect
{
    public override void DoEffect(GameObject user, int damage, Vector2 direction = default(Vector2))
    {
        if(direction == default(Vector2)) return;

        Collider2D collider = user.GetComponent<Collider2D>();
        RaycastHit2D rayHit;
        Vector2 start = user.transform.position;

        collider.enabled = false;
        GameManager.singleton.DisableCardColliders();

        rayHit = Physics2D.Raycast(start, direction);

        Debug.Log(rayHit.transform);

        collider.enabled = true;
        GameManager.singleton.EnableCardColliders();

        Debug.Log(rayHit.transform);

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
}