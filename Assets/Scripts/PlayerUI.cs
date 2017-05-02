using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;
    public Text apText;
    public Text scoreText;

    Player player;

    private void Start()
    {
        hpText.text = "0";
        apText.text = "0";
        scoreText.text = "0";
    }

    private void Update()
    {
        if(player)
        {
            hpText.text = player.health.ToString();
            apText.text = player.armor.ToString();
            scoreText.text = player.score.ToString();
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
}
