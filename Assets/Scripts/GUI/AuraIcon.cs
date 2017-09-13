using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraIcon : MonoBehaviour
{
    public Sprite defaultIcon;
    public Entity owner;

    TooltipCreatorUILayer tooltipCreator;
    Image icon;
    Text durText;

    private void Awake()
    {
        tooltipCreator = GetComponent<TooltipCreatorUILayer>();
        icon = GetComponent<Image>();
        durText = GetComponentInChildren<Text>();
    }

    public void SetOwner(Entity newOwner)
    {
        owner = newOwner;
    }

    public Aura GetAura()
    {
        int slot = transform.GetSiblingIndex();
        return owner.GetAura(slot);
    }

    public void UpdateIcon()
    {
        Aura aura = GetAura();

        if(aura != null)
        {
            tooltipCreator.enabled = true;
            tooltipCreator.SetItem(aura);

            if(aura.durationRemaining <= 20)
            {
                durText.enabled = true;
                durText.text = aura.durationRemaining.ToString();
            }
            else
            {
                durText.enabled = false;
            }

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
