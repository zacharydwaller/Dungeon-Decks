using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/HealthIncrease")]
public class HealthIncrease : CardEffect
{
    public int amount;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2))
    {
        user.GetComponent<Entity>().health += amount;
    }

    public override int GetEffectAmount()
    {
        return amount;
    }
}
