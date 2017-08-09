using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraIcon : MonoBehaviour
{
    private Aura aura;

    public void SetAura(Aura newAura)
    {
        aura = newAura;
        GetComponent<TooltipCreatorUILayer>().SetItem(aura);
        GetComponent<Image>().sprite = aura.effect.icon;
    }

    public Aura GetAura()
    {
        return aura;
    }
}
