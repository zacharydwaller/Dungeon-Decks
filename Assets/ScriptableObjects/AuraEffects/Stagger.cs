using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/Stagger")]
public class Stagger : AuraEffect
{
    public AuraEffect staggerDot;

    public override void OnAttacked(Entity attackedBy)
    {
        Enemy enemy = (Enemy) attackedBy;
        Player player = (Player) aura.owner;

        if(player.staggerDuration == 0)
        {
            Aura staggerDotAura = new Aura(player, staggerDot, 0, 10);
            player.ApplyAura(staggerDotAura);
        }

        player.staggerDamage += player.GetDamageTaken(enemy.info.magnitude);

        player.staggerDuration = Player.maxStaggerDuration;
    }
}
