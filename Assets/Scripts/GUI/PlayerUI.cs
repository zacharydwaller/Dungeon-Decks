using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;
    public Text apText;
    public Text deckText;
    public Text graveyardText;
    public Text scoreText;

    public Slider healthSlider;
    public Slider armorSlider;

    Player player;

    private void Start()
    {
        hpText.text = "0";
        apText.text = "0";
        deckText.text = "0";
        graveyardText.text = "0";
        scoreText.text = "0";

        healthSlider.maxValue = 0;
        armorSlider.maxValue = 0;
    }

    private void Update()
    {
        if(player)
        {
            hpText.text = player.health.ToString();
            apText.text = player.armor.ToString();
            deckText.text = player.deck.Count.ToString();
            graveyardText.text = player.graveyard.Count.ToString();
            scoreText.text = player.score.ToString();

            UpdateSlider(healthSlider, player.health);
            UpdateSlider(armorSlider, player.armor);
        }
        else
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj)
                player = playerObj.GetComponent<Player>();
            else
            {
                hpText.text = "0";
            }
        }
    }

    private void UpdateSlider(Slider slider, int stat)
    {
        int _stat = stat;

        if(slider.value != _stat)
        {
            if(_stat > slider.maxValue)
            {
                slider.maxValue = _stat;
            }

            slider.value = _stat;
        }
    }
}
