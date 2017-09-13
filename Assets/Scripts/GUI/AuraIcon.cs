using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraIcon : MonoBehaviour
{
    public Sprite defaultIcon;

    TooltipCreatorUILayer tooltipCreator;
    Image icon;
    Text durText;

    private void Awake()
    {
        tooltipCreator = GetComponent<TooltipCreatorUILayer>();
        icon = GetComponent<Image>();
        durText = GetComponentInChildren<Text>();
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

            durText.enabled = true;
            durText.text = aura.durationRemaining.ToString();

            icon.sprite = aura.effect.icon;
        }
        else
        {
            tooltipCreator.enabled = false;
            durText.enabled = false;
            icon.sprite = defaultIcon;
        }
    }
}
