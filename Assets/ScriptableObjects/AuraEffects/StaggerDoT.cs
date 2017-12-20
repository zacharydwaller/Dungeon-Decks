using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/StaggerDoT")]
public class StaggerDot : AuraEffect
{
    public override void Tick()
    {
        Player player = (Player) aura.owner;

        player.TakeStaggerDamage();

        player.staggerDamage -= player.staggerTickDamage;
        player.staggerDuration--;

        aura.magnitude = player.staggerDamage;
        aura.durationRemaining = player.staggerDuration;
    }

    public override void OnRemove()
    {
        Player player = (Player) aura.owner;

        player.staggerDamage = 0;
        player.staggerDuration = 0;
    }
}