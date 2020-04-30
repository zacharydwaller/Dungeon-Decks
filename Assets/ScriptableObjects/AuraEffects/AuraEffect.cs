using UnityEngine;

public abstract class AuraEffect : ScriptableObject
{
    public enum DamageType
    {
        None, Burn, Poison
    }

    public DamageType damageType;

    /*
     * Tooltip Description Tags
     * %m - Magnitude
     * %s - Max Duration (s for secondary to be consistent CardEffect tag)
     * %d - Duration Remaining
     * %r - Ratio, Magnitude/Max Duration
     */
    public string tooltipDescription;

    public bool isHarmful;

    public Sprite icon;
    public Aura aura;

    public virtual void Tick() { }
    public virtual void OnAdd() { }
    public virtual void OnRemove() { }
    public virtual void OnAttacked(Entity attackedBy) { }
    public virtual void OnTakenDamage(float amount) { }

    public virtual void AddStack(float amount)
    {
        aura.RefreshDuration();
    }
}
