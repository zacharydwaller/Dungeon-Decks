using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraTooltip : Tooltip
{
    public Text descText;

    Aura aura;

    private void Update()
    {
        if(aura != null)
        {
            descText.text = aura.GetTooltipDescription();
        }
    }

    public override void SetItem(object newAura)
    {
        aura = (Aura) newAura;
        if(aura != null)
        {
            descText.text = aura.GetTooltipDescription();
        }
    }
}
