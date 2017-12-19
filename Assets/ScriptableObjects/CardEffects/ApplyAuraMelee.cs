using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/ApplyAuraMelee")]
public class ApplyAuraMelee : CardEffect
{
    public AuraEffect auraEffect;
    
    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2), float magnitude = 0, int duration = 0)
    {
        if(direction == default(Vector2)) return;

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
                Aura aura = new Aura(ent, auraEffect, magnitude, duration);
                ent.ApplyAura(aura);
            }
        }
    }
}
