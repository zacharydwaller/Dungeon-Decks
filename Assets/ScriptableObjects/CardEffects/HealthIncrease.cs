using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/HealthIncrease")]
public class HealthIncrease : CardEffect
{
    public override void DoEffect(GameObject user, int amount, Vector2 direction = default(Vector2))
    {
        user.GetComponent<Entity>().health += amount;
    }
}
