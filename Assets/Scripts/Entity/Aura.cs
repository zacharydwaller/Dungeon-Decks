using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura
{
    public AuraEffect effect;

    public Entity owner;
    public int magnitude;
    public int duration;

    public int durationRemaining;

    public Aura(Entity newOwner, AuraEffect newEffect, int newMagnitude, int newDuration)
    {
        owner = newOwner;
        effect = newEffect;
        magnitude = newMagnitude;
        duration = durationRemaining = newDuration;

        effect.aura = this;
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

    public string GetTooltipDescription()
    {
        string ret = effect.tooltipDescription;

        ret = ret.Replace("%m", magnitude.ToString());
        ret = ret.Replace("%s", duration.ToString());
        ret = ret.Replace("%d", durationRemaining.ToString());
        ret = ret.Replace("%r", Mathf.RoundToInt((float) magnitude / duration).ToString());

        if(effect.GetType() == typeof(Buff))
        {
            Buff buff = (Buff) effect;
            ret = ret.Replace("%stat", (buff.stat.ToString()));
        }

        return ret;
    }
}
