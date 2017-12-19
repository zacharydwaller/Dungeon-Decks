using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/ApplyAuraSelf")]
public class ApplyAuraSelf : CardEffect
{
    public AuraEffect auraEffect;
    
    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2), float magnitude = 0, int duration = 0)
    {
        Entity ent = user.GetComponent<Entity>();
        Aura aura = new Aura(ent, auraEffect, magnitude, duration);

        ent.ApplyAura(aura);
    }
}
