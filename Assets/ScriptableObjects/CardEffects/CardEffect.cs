using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public bool isSelfCast;
    public bool isRanged;

    /*
     * Description Tags
     * %m - Magnitude
     * %s - Secondary Magnitude
     * %r - Ratio, Magnitude / Secondary
     */
    public string rawDescription;

    public abstract void DoEffect(GameObject user, Vector2 direction = default(Vector2), float magnitude = 0, int secondaryMagnitude = 0);
}
