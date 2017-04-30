﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int armor;

    public int selectedCard;
    public Card[] hand;
    public ArrayList deck;
    public ArrayList graveyard;

    private int punchIndex = 4;

    Vector2 prevInput;

    protected override void Start()
    {
        base.Start();

        health = 10;
        armor = 0;

        hand = new Card[5];
        deck = new ArrayList();
        graveyard = new ArrayList();

        // Player will always have punch in hand[4]
        // Punch doesn't go into graveyard when used and can't be discarded
        hand[punchIndex] = CardDatabase.GetPunchCard();

        //TODO remove this
        for(int i = 0; i < punchIndex; i++)
        {
            hand[i] = CardDatabase.GetPunchCard();
        }

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

    protected override void TakeDamage(int amount)
    {
        // Health damage rounds up, armor damage rounds down
        health -= (int) ((amount / 2.0f) + 0.5f);
        armor -= (int) amount / 2;

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

                selectedCard = punchIndex;
            }
        }
        else
        {
            selectedCard = num;
        }
    }

    public override void DoTurn()
    {
        Vector2 input = new Vector2();
        input.x = Mathf.Round(Input.GetAxisRaw("Horizontal"));
        input.y = Mathf.Round(Input.GetAxisRaw("Vertical"));

        // Check spacebar
        if(Input.GetKey(KeyCode.Space))
        {
            if(hand[selectedCard].target == Card.Target.Self)
            {
                DoCardEffect();
            }
            GameManager.singleton.EndPlayerTurn();
            return;
        }

        // Check WASD
        if(!isMoving && (input.x != 0 || input.y != 0))
        {
            // If selected card is ranged, use it
            if(hand[selectedCard].target == Card.Target.Ranged)
            {
                DoCardEffect();
            }
            // Otherwise move/attack
            else
            {
                RaycastHit2D rayHit;
                if(!Move(input, out rayHit))
                {
                    // Hit something
                    if(rayHit.transform.tag == "Enemy")
                    {
                        // Do attack 
                        Attack(hand[selectedCard].effectAmount, input, rayHit.transform.GetComponent<Enemy>());
                        DiscardCard(selectedCard);
                    }
                }
            }

            GameManager.singleton.EndPlayerTurn();
        }
    }

    public void DoCardEffect()
    {

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
}
