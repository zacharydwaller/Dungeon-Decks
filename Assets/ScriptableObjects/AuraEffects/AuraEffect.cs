using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AuraEffect : ScriptableObject
{
    /*
     * Tooltip Description Tags
     * %m - Magnitude
     * %s - Max Duration (s for secondary to be consistent CardEffect tag)
     * %d - Duration Remaining
     * %r - Ratio, Magnitude/Max Duration
     */
    public string tooltipDescription;

    public bool isHarmful;

    public Sprite icon;
    public Aura aura;

    public virtual void Tick() { }
    public virtual void OnAdd() { }
    public virtual void OnRemove() { }

    public virtual string GetTooltip()
    {
        string ret = tooltipDescription;

        ret = ret.Replace("%m", aura.magnitude.ToString());
        ret = ret.Replace("%s", aura.duration.ToString());
        ret = ret.Replace("%d", aura.durationRemaining.ToString());
        ret = ret.Replace("%r", Mathf.RoundToInt(aura.magnitude / aura.duration).ToString());

        return ret;
    }
}
