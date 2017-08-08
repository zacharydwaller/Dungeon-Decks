using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura
{
    public AuraEffect effect;

    Entity owner;
    public int magnitude;
    public int duration;

    public int durationRemaining;

    public Aura(Entity newOwner, AuraEffect newEffect, int newMagnitude, int newDuration)
    {
        owner = newOwner;
        effect = newEffect;
        magnitude = newMagnitude;
        duration = durationRemaining = newDuration;
    }

    // Returns true if finished
    public bool Tick()
    {
        effect.Tick(owner, magnitude, duration);

        durationRemaining--;

        if(durationRemaining == 0) return true;
        else return false;
    }
}
