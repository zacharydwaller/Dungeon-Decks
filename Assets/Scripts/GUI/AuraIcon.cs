using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraIcon : MonoBehaviour
{
    public Sprite defaultIcon;

    TooltipCreatorUILayer tooltipCreator;
    Image icon;

    private void Awake()
    {
        tooltipCreator = GetComponent<TooltipCreatorUILayer>();
        icon = GetComponent<Image>();
    }

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
            tooltipCreator.enabled = true;
            tooltipCreator.SetItem(aura);
            icon.sprite = aura.effect.icon;
        }
        else
        {
            tooltipCreator.enabled = false;
            icon.sprite = defaultIcon;
        }
    }
}
