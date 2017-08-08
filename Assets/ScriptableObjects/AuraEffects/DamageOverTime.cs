using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/DoT")]
public class DamageOverTime : AuraEffect
{
    public override void Tick(GameObject owner, int magnitude, int duration)
    {
        owner.GetComponent<Entity>().TakeDamage(Mathf.RoundToInt(magnitude / duration));
    }
}
