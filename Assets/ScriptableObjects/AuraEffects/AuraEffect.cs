using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AuraEffect : ScriptableObject
{
    public abstract void Tick(Entity owner, int magnitude, int duration);
}
