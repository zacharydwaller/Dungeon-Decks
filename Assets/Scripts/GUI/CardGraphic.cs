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
    public Text rangedText;

    private Player player;

    private float updateDelay = 0.33f;
    private float nextUpdate = 0;
    
    public bool isPlaceholder;

    private void Start()
    {
        player = GameManager.singleton?.player;
    }
    
    /// <summary>
    ///     TODO: Does this stuff need to be in Update?
    ///     Can we just update graphic and outline as needed?
    /// </summary>
    private void Update()
    {
        if(player?.selectedCard == handIndex)
        {
            selectedOutline.enabled = true;
        }
        else
        {
            selectedOutline.enabled = false;
        }

        if(Time.time >= nextUpdate)
        {
            UpdateGraphic();
            nextUpdate = Time.time + updateDelay;
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
        card = newCard as CardInfo;

        if (card == null) return;

        nameText.text = card.cardName;
        baseGraphic.color = card.color;
        image.sprite = card.image;
        effectText.text = card.GetDescription();

        consumableText.enabled = card.isConsumable;
        rangedText.enabled = card.isRanged;
    }
}
