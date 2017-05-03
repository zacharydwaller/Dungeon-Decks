using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int armor;
    public int score;

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
        score = 0;

        data.type = Entity.Type.Player;

        hand = new Card[5];
        deck = new ArrayList();
        graveyard = new ArrayList();

        // Player will always have punch in hand[4]
        // Punch doesn't go into graveyard when used and can't be discarded
        hand[punchIndex] = CardDatabase.GetPunchCard();

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
        if(Input.GetKey(KeyCode.Space))
        {
            // Use card if card is self-cast
            if(hand[selectedCard].target == Card.Target.Self)
            {
                DoCardEffect();

            }

            DrawCard();
            GameManager.singleton.EndPlayerTurn();
            return;
        }

        // Check WASD
        if(!isMoving && (input.x != 0 || input.y != 0))
        {
            // If selected card is ranged, use it
            if(hand[selectedCard].target == Card.Target.Ranged)
            {
                DoCardEffect(input);
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
                    // Hit enemy
                    if(rayHit.transform.tag == "Enemy")
                    {
                        DoCardEffect(input, rayHit.transform.GetComponent<Enemy>());
                    }
                    // Hit card
                    // Card case is elseif because don't want to move onto/collect a card
                    // with an enemy on it
                    else if(rayHit.transform.tag == "Card")
                    {
                        CollectCard(rayHit.transform.GetComponent<CardPickup>());
                        Move(input);
                    }
                    // Hit door
                    else if(rayHit.transform.tag == "Door")
                    {
                        GameManager.singleton.ChangeBoard(input);
                    }
                }
            }

            DrawCard();
            GameManager.singleton.EndPlayerTurn();
        }
    } 

    protected override void TakeDamage(int amount)
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


    // Read Direction keys such that diagonals are not possible
    public void ReadDirectionKeys(out Vector2 output)
    {
        output = new Vector2(0, 0);

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            output.y = 1;
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            output.y = -1;
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            output.x = -1;
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            output.x = 1;
        }
    }

    public void DoCardEffect(Vector2 dir = default(Vector2), Entity enemyHit = null)
    {
        Card card = hand[selectedCard];

        if(card.target == Card.Target.Self)
        {
            if(card.targetStat == Card.TargetStat.Health)
            {
                health += card.effectAmount;
            }
            if(card.targetStat == Card.TargetStat.Armor)
            {
                armor += card.effectAmount;
            }
        }
        else if(card.target == Card.Target.Ranged)
        {
            // Do ranged attack
        }
        else if(card.target == Card.Target.Melee)
        {
            Attack(hand[selectedCard].effectAmount, dir, enemyHit);
        }

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
            Card card = (Card) deck[0];

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
    public bool PutCardInHand(Card card)
    {
        for(int i = 0; i < punchIndex; i++)
        {
            if(hand[i] == null)
            {
                hand[i] = card;
                return true;
            }
        }

        return true;
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

        for(int i = 0; i < deck.Count; i++)
        {
            Card tmp = (Card) deck[i];
            int swap = Random.Range(0, deck.Count);

            deck[i] = deck[swap];
            deck[swap] = tmp;
        }

        // Fill hand
        while(DrawCard());
    }
}
