using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/HoT")]
public class HealOverTime : AuraEffect
{
    public override void Tick()
    {
        float tickMagnitude = aura.magnitude / aura.initialDuration;

        aura.owner.health += tickMagnitude;
    }
}
