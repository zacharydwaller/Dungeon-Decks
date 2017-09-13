using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/CounterAttack")]
public class CounterAttack : AuraEffect
{
    public override void OnAttacked(Entity attackedBy)
    {
        Vector2 dir = attackedBy.transform.position - aura.owner.transform.position;

        if(dir.magnitude <= 1)
        {
            //aura.owner.DoAttackAnimation(dir);
            attackedBy.TakeDamage(aura.magnitude);
            aura.durationRemaining = 1;
        }
    }
}
