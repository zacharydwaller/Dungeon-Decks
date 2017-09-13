using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/Thorns")]
public class Thorns : AuraEffect
{
    public override void OnAttacked(Entity attackedBy)
    {
        attackedBy.TakeDamage(aura.magnitude);
    }
}
