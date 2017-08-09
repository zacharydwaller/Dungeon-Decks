using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;
    public Text apText;
    public Text dmgText;
    public Text drText;
    public Text deckText;
    public Text graveyardText;

    public Text scoreText;

    public Slider healthSlider;
    public Slider armorSlider;

    public GameObject auraPanel;

    public GameObject auraIconRef;

    private float updateDelay = 0.33f;
    private float nextUpdate;

    private Player player;

    private void Start()
    {
        hpText.text         = "0";
        apText.text         = "0";
        dmgText.text        = "0";
        drText.text         = "0";
        deckText.text       = "0";
        graveyardText.text  = "0";
        scoreText.text      = "0";

        healthSlider.maxValue   = 0;
        armorSlider.maxValue    = 0;

        nextUpdate = 0f;
    }

    private void Update()
    {
        if(Time.time < nextUpdate) return;
        nextUpdate = Time.time + updateDelay;

        if(player)
        {
            hpText.text        = player.health.ToString();
            apText.text        = player.armor.ToString();
            dmgText.text       = player.dmgBonus.ToString();
            drText.text        = player.dmgReduction.ToString();
            deckText.text      = player.deck.Count.ToString();
            graveyardText.text = player.graveyard.Count.ToString();
            scoreText.text     = player.score.ToString();

            UpdateSlider(healthSlider, player.health);
            UpdateSlider(armorSlider, player.armor);

            UpdateAuraPanel();
        }
        else
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj)
                player = playerObj.GetComponent<Player>();
            else
            {
                hpText.text = "0";
                UpdateSlider(healthSlider, 0);
            }
        }
    }

    private void UpdateSlider(Slider slider, int stat)
    {
        if(slider.value != stat)
        {
            if(stat > slider.maxValue)
            {
                slider.maxValue = stat;
            }

            slider.value = stat;
        }
    }

    private void UpdateAuraPanel()
    {
        for(int i = auraPanel.transform.childCount - 1; i >= 0; i--)
        {
            bool auraExists = false;

            foreach(Aura aura in player.auras)
            {
                if(aura == auraPanel.transform.GetChild(i).GetComponent<AuraIcon>().GetAura())
                {
                    auraExists = true;
                    break;
                }
            }

            if(!auraExists)
            {
                Destroy(auraPanel.transform.GetChild(i).gameObject);
            }
        }

        foreach(Aura aura in player.auras)
        {
            bool iconExists = false;

            for(int i = 0; i < auraPanel.transform.childCount; i++)
            {
                if(aura == auraPanel.transform.GetChild(i).GetComponent<AuraIcon>().GetAura())
                {
                    iconExists = true;
                    break;
                }
            }

            if(!iconExists)
            {
                GameObject icon = Instantiate(auraIconRef);
                icon.transform.SetParent(auraPanel.transform);
                icon.GetComponent<AuraIcon>().SetAura(aura);
            }
        }
    }
}
