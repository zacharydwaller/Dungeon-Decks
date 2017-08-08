using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AuraEffect : ScriptableObject
{
    public string rawDescription;

    public abstract void Tick(GameObject owner, int magnitude, int duration);
}
