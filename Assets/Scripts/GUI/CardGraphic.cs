using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGraphic : Tooltip
{
    [HideInInspector]
    public int handIndex = -1;

    CardInfo card;

    public Text nameText;
    public Text effectText;
    public Image image;
    public Image baseGraphic;
    public Outline selectedOutline;
    public Text consumableText;

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

    public void UpdateGraphic()
    {
        SetItem(card);
    }

    public CardInfo GetCard() { return card; }

    public override void SetItem(object newCard)
    {
        card = (CardInfo) newCard;
        nameText.text = card.cardName;
        baseGraphic.color = card.color;
        image.sprite = card.image;
        effectText.text = card.description;

        consumableText.enabled = card.isConsumable;
    }
}
