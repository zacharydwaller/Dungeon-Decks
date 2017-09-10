using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/DoT")]
public class DamageOverTime : AuraEffect
{
    public override void Tick(Entity owner, int magnitude, int duration, int durationRemaining)
    {
        int tickMagnitude = Mathf.RoundToInt((float) magnitude / duration);

        owner.TakeDamage(tickMagnitude);
    }
}
