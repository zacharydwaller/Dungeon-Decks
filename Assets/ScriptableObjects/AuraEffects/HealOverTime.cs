using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/HoT")]
public class HealOverTime : AuraEffect
{
    public override void Tick()
    {
        int tickMagnitude = Mathf.RoundToInt((float) aura.magnitude / aura.duration);

        aura.owner.health += tickMagnitude;
    }
}
