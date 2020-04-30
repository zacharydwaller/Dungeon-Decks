using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura
{
    public AuraEffect effect;

    public Entity owner;
    public float magnitude;

    public int initialDuration;
    public int durationRemaining;

    public Aura(Entity newOwner, AuraEffect newEffect, float newMagnitude, int newDuration)
    {
        owner = newOwner;

        effect = newEffect;
        effect.aura = this;

        magnitude = newMagnitude;
        initialDuration = durationRemaining = newDuration;

        owner.TakingDamage += OnTakenDamage;
    }

    // Returns true if finished
    public bool Tick()
    {
        effect.Tick();

        durationRemaining--;

        if(durationRemaining == 0) return true;
        else return false;
    }

    public void OnAdd()
    {
        effect.OnAdd();
    }

    public void OnRemove()
    {
        effect.OnRemove();
    }

    public void AddStack(float amount)
    {
        effect.AddStack(amount);
    }

    public void RefreshDuration()
    {
        durationRemaining = initialDuration;
    }

    public void OnTakenDamage(object sender, float amount)
    {
        effect.OnTakenDamage(amount);
    }

    /*
     * m - Magnitude
     * s - Max Duration
     * d - Duration Remaining
     * r - Ratio magnitude/damage
     */
    public string GetTooltipDescription()
    {
        string ret = effect.tooltipDescription;

        ret = ret.Replace("%m", magnitude.ToString("N2"));
        ret = ret.Replace("%s", initialDuration.ToString());
        ret = ret.Replace("%d", durationRemaining.ToString());
        ret = ret.Replace("%r", (magnitude / initialDuration).ToString("N2"));

        if(effect.GetType() == typeof(Buff))
        {
            Buff buff = (Buff) effect;
            ret = ret.Replace("%stat", (buff.stat.ToString()));
        }

        return ret;
    }
}
