using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/Ignite")]
public class Ignite : CardEffect
{
    public Sprite dotIcon;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2), int magnitude = 0, int secondaryMagnitude = 0)
    {
        if(direction == default(Vector2)) return;

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

                int totalDamage = 0;
                int avgDuration = 0;
                int numBurns = 0;

                foreach(Aura aura in auras)
                {
                    if(aura.effect.damageType == AuraEffect.DamageType.Burn)
                    {
                        totalDamage += Mathf.RoundToInt((float) aura.magnitude / aura.duration) * aura.durationRemaining;
                        avgDuration += aura.durationRemaining;
                        numBurns++;

                        aura.durationRemaining = 1;
                    }
                }

                totalDamage = Mathf.RoundToInt((float) totalDamage / 2);
                avgDuration = avgDuration / numBurns;

                dot.damageType = AuraEffect.DamageType.Burn;
                dot.icon = dotIcon;

                ent.ApplyAura(new Aura(ent, dot, totalDamage / 2, avgDuration));
                ent.TakeDamage(totalDamage / 2);
            }
        }

        
    }
}
