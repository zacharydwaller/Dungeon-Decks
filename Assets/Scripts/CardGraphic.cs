using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGraphic : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text effectText;
    public Image image;
    public Image baseGraphic;

    private Player player;
    private bool isSelected;

    public bool isPlaceholder;

    private void Start()
    {
        player = GameManager.singleton.player;
    }

    private void Update()
    {
        if(player.hand[player.selectedCard] == card)
        {
            transform.localScale = new Vector3(1, 1.2f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void UpdateCardGraphic()
    {
        SetName();
        SetColor();
        SetImage();
        SetEffectText();
    }

    public void SetName()
    {
        nameText.text = card.name;
    }

    public void SetColor()
    {
        baseGraphic.color = card.color;
    }

    public void SetImage()
    {
        image.sprite = card.image;
    }

    public void SetEffectText()
    {
        string text = string.Empty;

        // Self target cards
        // +X HP, +X AP
        if(card.target == Card.Target.Self)
        {
            text += "+" + card.effectAmount;

            if(card.targetStat == Card.TargetStat.Health)
            {
                text += "HP";
            }
            else
            {
                text += "AP";
            }
        }
        // Enemy target cards
        // 3 DMG, 3 R-DMG
        else
        {
            text += card.effectAmount + " ";

            if(card.target == Card.Target.Ranged)
            {
                text += "R-";
            }

            text += "DMG";
        }
    }

    

    
}
