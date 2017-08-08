using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public bool isSelfCast;
    public bool isRanged;
    public string rawDescription;

    public abstract void DoEffect(GameObject user, Vector2 direction = default(Vector2), int magnitude = 0, int secondaryMagnitude = 0);
}
