using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    public AuraEffect effect;

    GameObject owner;
    public int magnitude;
    public int duration;

    public int durationRemaining;

    private void Start()
    {
        durationRemaining = duration;
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
