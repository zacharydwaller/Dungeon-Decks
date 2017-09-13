using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraIcon : MonoBehaviour
{
    public Sprite defaultIcon;

    public Aura GetAura()
    {
        int slot = transform.GetSiblingIndex();
        return GameManager.singleton.player.GetAura(slot);
    }

    public void UpdateIcon()
    {
        Aura aura = GetAura();

        if(aura != null)
        {
            GetComponent<TooltipCreatorUILayer>().SetItem(aura);
            GetComponent<Image>().sprite = aura.effect.icon;
        }
        else
        {
            GetComponent<Image>().sprite = defaultIcon;
        }
    }
}
