using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/HoT")]
public class HealOverTime : AuraEffect
{
    public override void Tick(Entity owner, int magnitude, int duration)
    {
        owner.health += Mathf.RoundToInt(magnitude / duration);
    }
}
