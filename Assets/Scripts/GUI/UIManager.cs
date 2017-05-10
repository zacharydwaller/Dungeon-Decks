﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public Text gameOverScoreText;

    [Header("Hand Panel")]
    public GameObject handPanel;
    public GameObject placeholderRef;
    public GameObject cardRef;
    public GameObject[] hand;


    private void Start()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);

        hand = new GameObject[5];
        AddPlaceholders();
    }

    private void Update()
    {
        if(GameManager.singleton.player != null)
        {
            UpdateHand();
        }
    }

    private void UpdateHand()
    {
        CardInfo[] playerHand = GameManager.singleton.player.hand;
        if(playerHand == null) return;

        for(int i = 0; i < 5; i++)
        {
            //bool placeEmpty = hand[i].GetComponent<CardGraphic>().isPlaceholder;
            bool placeEmpty = !hand[i].GetComponent<CardGraphic>();

            // New card at placeholder spot
            if(placeEmpty && playerHand[i] != null)
            {
                AddCardToHand(i);
            }
            // Removed card
            else if(!placeEmpty && playerHand[i] == null)
            {
                RemoveCardFromHand(i);
            }

            // Ensure card in correct slot
            if(hand[i].transform.GetSiblingIndex() != i)
            {
                hand[i].transform.SetSiblingIndex(i);
            }

            // Ensure card graphic correct
            CardGraphic cg = hand[i].GetComponent<CardGraphic>();
            if(cg != null && cg.GetCard() != playerHand[i])
            {
                cg.SetItem((CardInfo) playerHand[i]);
            }
        }
    }

    private void AddCardToHand(int num)
    {
        GameObject oldCard = (GameObject) hand[num];
        GameObject newCard = Instantiate(cardRef);
        CardGraphic cardGraphic = newCard.GetComponent<CardGraphic>();

        newCard.transform.SetParent(handPanel.transform, false);
        cardGraphic.handIndex = num;
        cardGraphic.SetItem((CardInfo) GameManager.singleton.player.hand[num]);

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

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
        gameOverScoreText.text = GameManager.singleton.player.score.ToString();
    }

    public void ShowPauseUI()
    {
        pauseUI.SetActive(true);
    }

    public void HidePauseUI()
    {
        pauseUI.SetActive(false);
    }
}
