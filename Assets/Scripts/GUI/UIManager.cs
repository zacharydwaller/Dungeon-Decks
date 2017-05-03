using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Hand Panel")]
    public GameObject handPanel;
    public GameObject placeholderRef;
    public GameObject cardRef;
    public GameObject[] hand;

    private void Start()
    {
        hand = new GameObject[5];
        AddPlaceholders();
    }

    private void Update()
    {
        UpdateHand();
    }

    private void UpdateHand()
    {
        Card[] playerHand = GameManager.singleton.player.hand;
        if(playerHand == null) return;

        for(int i = 0; i < 5; i++)
        {
            //bool placeEmpty = hand[i].GetComponent<CardGraphic>().isPlaceholder;
            bool placeEmpty = !hand[i].GetComponent<CardGraphic>();

            // New card at placeholder spot
            if(placeEmpty && playerHand[i] != null)
            {
                AddCardToHand(i);
                continue;
            }
            // Removed card
            if(!placeEmpty && playerHand[i] == null)
            {
                RemoveCardFromHand(i);
                continue;
            }
        }
    }

    private void AddCardToHand(int num)
    {
        GameObject oldCard = (GameObject) hand[num];
        GameObject newCard = Instantiate(cardRef);
        CardGraphic cardGraphic = newCard.GetComponent<CardGraphic>();

        newCard.transform.SetParent(handPanel.transform, false);
        cardGraphic.card = GameManager.singleton.player.hand[num];
        cardGraphic.UpdateCardGraphic();

        newCard.transform.SetSiblingIndex(num);
        Destroy(oldCard);

        hand[num] = newCard;
    }

    private void RemoveCardFromHand(int num)
    {
        GameObject oldCard = (GameObject) hand[num];
        int siblingIndex = oldCard.transform.GetSiblingIndex();
        GameObject placeholder = Instantiate(placeholderRef);

        placeholder.transform.SetParent(handPanel.transform, false);
        placeholder.transform.SetSiblingIndex(siblingIndex);
        Destroy(oldCard);

        hand[num] = placeholder;
    }

    private void AddPlaceholders()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject placeholder = Instantiate(placeholderRef);
            placeholder.transform.SetParent(handPanel.transform, false);
            hand[i] = placeholder;
        }
    }
}
