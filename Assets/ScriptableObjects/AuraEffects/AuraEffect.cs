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

    public abstract void Tick();
    public virtual void OnAdd() { }
    public virtual void OnRemove() { }
}
