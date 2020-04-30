using UnityEngine;

/// <summary>
///     Stagger blocks all incoming damage to the player and spreads it over the next 10 turns.
///     Taking additional damage will refresh the duration and add a new stack.
///     Old damage applications will expire and remove themselves from the total damage taken per turn.
/// </summary>
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
