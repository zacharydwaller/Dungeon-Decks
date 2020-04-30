using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/Stagger")]
public class Stagger : AuraEffect
{
    public AuraEffect StaggerDot;

    public const int MaxStaggerDuration = 10;

    public override void OnTakenDamage(float amount)
    {
        var player = aura.owner as Player;
        player.incomingDamage = 0;

        var existingStagger = player.GetAura<StaggerDot>();

        if (existingStagger != null)
        {
            existingStagger.AddStack(amount);
        }
        else
        {
            player.ApplyAura(new Aura(aura.owner, StaggerDot, amount, MaxStaggerDuration));
        }
    }
}
