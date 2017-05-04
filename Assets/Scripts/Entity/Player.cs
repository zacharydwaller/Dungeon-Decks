﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int armor;
    public int score;

    public CardInfo punchCard;
    public int selectedCard;
    public CardInfo[] hand;
    public ArrayList deck;
    public ArrayList graveyard;

    private int punchIndex = 4;

    Vector2 prevInput;

    protected override void Start()
    {
        base.Start();

        health = 10;
        armor = 0;
        score = 0;

        data.type = Entity.Type.Player;

        hand = new CardInfo[5];
        deck = new ArrayList();
        graveyard = new ArrayList();

        // Player will always have punch in hand[4]
        // Punch doesn't go into graveyard when used and can't be discarded
        hand[punchIndex] = punchCard;

        selectedCard = punchIndex;

        prevInput = Vector2.zero;
    }

    private void Update()
    {
        CheckSelectCard();

        if(GameManager.singleton.isPlayerTurn)
        {
            DoTurn();
        }
    }


    public override void DoTurn()
    {
        Vector2 input;
        ReadDirectionKeys(out input);

        // Check spacebar
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Use card if card is self-cast
            if(hand[selectedCard].isSelfCast)
            {
                UseCard();
            }
            else
            {
                DrawCard();
            }

            GameManager.singleton.EndPlayerTurn();
            return;
        }

        // Check WASD
        if(!isMoving && (input.x != 0 || input.y != 0))
        {
            bool usedCard = false; // Only draw card if player hasn't used card this turn

            // If player holding shift, use card
            if(Input.GetKey(KeyCode.LeftShift))
            {
                UseCard(input);
                usedCard = true;
            }
            // Otherwise move/attack
            else
            {
                RaycastHit2D rayHit;
                if(CheckMove(input, out rayHit))
                {
                    Move(input);
                }
                // Hit something
                else
                {
                    // Hit enemy, use card on him
                    if(rayHit.transform.tag == "Enemy")
                    {
                        UseCard(input);
                        usedCard = true;
                    }
                    // Hit card, pick up card
                    else if(rayHit.transform.tag == "Card")
                    {
                        CollectCard(rayHit.transform.GetComponent<CardPickup>());
                        Move(input);
                    }
                    // Hit door, go through door
                    else if(rayHit.transform.tag == "Door")
                    {
                        GameManager.singleton.ChangeBoard(input);
                    }
                }
            }

            if(!usedCard)
            {
                DrawCard();
            }

            GameManager.singleton.EndPlayerTurn();
        }
    } 

    public override void TakeDamage(int amount)
    {
        if(amount == 1)
        {
            armor -= 1;
        }
        else
        {
            // Health damage rounds up, armor damage rounds down
            health -= (int) ((amount / 2.0f) + 0.5f);
            armor -= (int) amount / 2;
        }

        // If damage breaks armor, apply rest of damage to health
        if(armor < 0)
        {
            health += armor;
            armor = 0;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SelectCard(int num)
    {
        if(num < 0 || num > punchIndex) return;
        selectedCard = num;
    }

    public void CheckSelectCard()
    {
        int num =-1;

        if(Input.GetKeyDown(KeyCode.Alpha1))
            num = 0;
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            num = 1;
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            num = 2;
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            num = 3;
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            num = 4;

        // Player didn't press number or card doesn't exist
        if(num < 0 || hand[num] == null) return;

        // If player holding shift, discard card instead of select
        if(Input.GetKey(KeyCode.LeftShift))
        {
            // Dont discard Punch
            if(num != punchIndex)
            {
                DiscardCard(num);

                SelectCard(punchIndex);
            }
        }
        else
        {
            SelectCard(num);
        }
    }


    // Read Direction keys such that diagonals are not possible
    public void ReadDirectionKeys(out Vector2 output)
    {
        output = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Keypad8))
        {
            output.y = 1;
        }
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Keypad2))
        {
            output.y = -1;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4))
        {
            output.x = -1;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6))
        {
            output.x = 1;
        }
    }

    public void UseCard(Vector2 dir = default(Vector2))
    {
        hand[selectedCard].DoEffect(gameObject, dir);
        DiscardCard(selectedCard);
    }

    public void CollectCard(CardPickup pickup)
    {
        // Try to put card directly in hand
        if(!PutCardInHand(pickup.card))
        {
            // Hand full, put in deck
            deck.Add(pickup.card);
        }

        pickup.Pickup();
    }

    public bool DrawCard()
    {
        if(deck.Count > 0)
        {
            CardInfo card = (CardInfo) deck[0];

            if(PutCardInHand(card))
            {
                deck.RemoveAt(0);
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    /*
     * Tries to put a card in the hand
     * Returns - true if card was placed in hand, false if hand full
     */
    public bool PutCardInHand(CardInfo card)
    {
        for(int i = 0; i < punchIndex; i++)
        {
            if(hand[i] == null)
            {
                hand[i] = card;
                return true;
            }
        }

        return false;
    }

    public void DiscardCard(int num)
    {
        if(num != punchIndex)
        {
            graveyard.Add(hand[num]);
            hand[num] = null;
            selectedCard = punchIndex;
        }
    }

    public bool HandFull()
    {
        for(int i = 0; i < punchIndex; i++)
        {
            if(hand[i] == null)
            {
                return false;
            }
        }

        return true;
    }

    // Puts cards from the graveyard back into the deck, and shuffles deck and fills hand
    public void Reshuffle()
    {
        deck.AddRange(graveyard);
        graveyard.Clear();

        for(int i = 0; i < deck.Count; i++)
        {
            CardInfo tmp = (CardInfo) deck[i];
            int swap = Random.Range(0, deck.Count);

            deck[i] = deck[swap];
            deck[swap] = tmp;
        }

        // Fill hand
        while(DrawCard());
    }
}
