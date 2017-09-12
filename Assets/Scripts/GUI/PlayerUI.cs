using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;
    public Text apText;

    public Text strText;
    public Text magText;
    public Text dexText;
    public Text enhText;

    public Text deckText;
    public Text graveyardText;

    public Text scoreText;

    public Slider healthSlider;
    public Slider armorSlider;

    public GameObject auraPanelObj;

    public GameObject auraIconRef;

    private float updateDelay = 0.1f;
    private float nextUpdate;

    [HideInInspector]
    public Player player;

    private AuraPanel auraPanel;

    private void Start()
    {
        hpText.text         = "0";
        apText.text         = "0";

        strText.text        = "0";
        magText.text        = "0";
        dexText.text        = "0";
        enhText.text        = "0";

        deckText.text       = "0";
        graveyardText.text  = "0";
        scoreText.text      = "0";

        healthSlider.maxValue   = 10;
        armorSlider.maxValue    = 0;

        healthSlider.value = 10;
        armorSlider.value = 0;

        nextUpdate = 0f;

        auraPanel = new AuraPanel(this);
    }

    private void Update()
    {
        if(Time.time < nextUpdate) return;
        nextUpdate = Time.time + updateDelay;

        if(player)
        {
            hpText.text         = player.health.ToString();
            apText.text         = player.armor.ToString();

            strText.text        = player.strength.ToString();
            magText.text        = player.magic.ToString();
            dexText.text        = player.dexterity.ToString();
            enhText.text        = player.enhancement.ToString();

            deckText.text       = player.deck.Count.ToString();
            graveyardText.text  = player.graveyard.Count.ToString();
            scoreText.text      = player.score.ToString();

            UpdateSlider(healthSlider, player.health);
            UpdateSlider(armorSlider, player.armor);

            auraPanel.Update();
        }
        else
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj)
            {
                player = playerObj.GetComponent<Player>();
                auraPanel.SetPlayer(player);
            }
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
}
