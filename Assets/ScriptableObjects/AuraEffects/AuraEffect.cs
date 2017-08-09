using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AuraEffect : ScriptableObject
{
    public bool isHarmful;

    public abstract void Tick(Entity owner, int magnitude, int duration);
}
