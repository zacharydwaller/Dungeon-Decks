using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGraphic : MonoBehaviour
{
    public CardInfo card;
    [HideInInspector]
    public int handIndex = -1;

    public Text nameText;
    public Text effectText;
    public Image image;
    public Image baseGraphic;
    public Outline selectedOutline;

    private Player player;
    
    public bool isPlaceholder;

    private void Start()
    {
        player = GameManager.singleton.player;
    }
    
    private void Update()
    {
        if(player.selectedCard == handIndex)
        {
            selectedOutline.enabled = true;
        }
        else
        {
            selectedOutline.enabled = false;
        }
    }

    public void SelectCard()
    {
        player.SelectCard(handIndex);
    }

    public void UpdateCardGraphic()
    {
        nameText.text = card.cardName;
        baseGraphic.color = card.color;
        image.sprite = card.image;
        effectText.text = card.effect.description;
    }
}
