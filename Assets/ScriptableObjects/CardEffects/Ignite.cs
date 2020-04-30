using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Ignite adds up all of the remaining Burn damage on the target.
///     Applies half that damage immediately and the other half as a DoT over the average remaining Burn duration.
/// </summary>
[CreateAssetMenu(menuName = "Database/CardEffect/Ignite")]
public class Ignite : CardEffect
{
    public Sprite dotIcon;

    public override void DoEffect(GameObject user, Vector2 direction = default, float magnitude = 0, int ignore = 0)
    {
        if(direction == default) return;

        Collider2D collider = user.GetComponent<Collider2D>();
        RaycastHit2D rayHit;
        Vector2 start = user.transform.position;

        collider.enabled = false;
        GameManager.singleton.DisableCardColliders();

        rayHit = Physics2D.Raycast(start, direction);

        collider.enabled = true;
        GameManager.singleton.EnableCardColliders();

        if(rayHit.transform != null)
        {
            Entity ent = rayHit.transform.GetComponent<Entity>();

            if(ent)
            {
                List<Aura> auras = ent.GetAuras();
                DamageOverTime dot = CreateInstance<DamageOverTime>();

                float totalDamage = 0;
                int totalDuration = 0;
                int numBurns = 0;

                foreach(Aura aura in auras)
                {
                    if(aura.effect.damageType == AuraEffect.DamageType.Burn)
                    {
                        totalDamage += (aura.magnitude / aura.initialDuration) * aura.durationRemaining;
                        totalDuration += aura.durationRemaining;
                        numBurns++;

                        aura.durationRemaining = 1;
                    }
                }

                var halfDamage = totalDamage * 0.5f;
                var avgDuration = totalDuration / numBurns;

                dot.damageType = AuraEffect.DamageType.Burn;
                dot.icon = dotIcon;

                ent.ApplyAura(new Aura(ent, dot, halfDamage, avgDuration));
                ent.TakeDamage(halfDamage);
            }
        }
    }
}
