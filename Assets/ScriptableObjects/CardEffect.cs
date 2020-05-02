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

    /// <summary>
    /// Description Tags:
    /// %m - Magnitude.
    /// %d - Duration.
    /// %r - Ratio, Magnitude / Duration.
    /// </summary>
    public string rawDescription;

    public abstract void DoEffect(GameObject user, Vector2 direction = default(Vector2), float magnitude = 0, int secondaryMagnitude = 0);

    public string MakeDescription(float magnitude, int duration)
    {
        string ret = rawDescription;

        ret = ret.Replace("%m", magnitude.ToString());
        ret = ret.Replace("%d", duration.ToString());

        var ratio = Mathf.RoundToInt(magnitude / Mathf.Max(1, duration));
        ret = ret.Replace("%r", ratio.ToString());

        return ret;
    }
}
