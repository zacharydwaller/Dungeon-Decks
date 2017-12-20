﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/DoT")]
public class DamageOverTime : AuraEffect
{
    public override void Tick()
    {
        float tickMagnitude = aura.magnitude / aura.duration;

        aura.owner.TakeDamage(tickMagnitude);
    }
}
