using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardDB/Database")]
public class CardDatabase : ScriptableObject
{
    public CardPool[] cardPools;

    /* Gets a card of a selected tier
     * Card Choice is weighted by item type and main/off stat
     * Main Stat:   50%
     * Off Stat:    20%
     * Potions:     20%
     * Other Stats: 0%
     * Relics:      10%
     * 
     * Type of Relic chosen is weighted 60/30/10 for Main/Off/Other stats
     */
    public CardInfo GetCardOfTier(int tier)
    {
        Player player = GameManager.singleton.player;
        int poolRoll, cardRoll;
        StatType pool;

        tier = Mathf.Min(tier, cardPools[0].tiers.Length - 1);

        poolRoll = Random.Range(0, 100);

        // Main Stat
        if(poolRoll < 50)
        {
            pool = player.mainStat;
        }
        // Off Stat
        else if(poolRoll < 70)
        {
            pool = player.offStat;
        }
        // Potion
        else if(poolRoll < 90)
        {
            pool = player.otherStats[0];
        }
        /*
        // Other Stat
        else if(poolRoll < 90)
        {
            pool = player.otherStats[1];
        }
        */
        // Relic
        else
        {
            poolRoll = Random.Range(0, 100);
            // Main Stat
            if(poolRoll < 60)
            {
                pool = player.mainStat;
            }
            // Off Stat
            else if(poolRoll < 90)
            {
                pool = player.offStat;
            }
            // Other Stat 1
            else if(poolRoll < 95)
            {
                pool = player.otherStats[0];
            }
            // Other Stat 2
            else
            {
                pool = player.otherStats[1];
            }

            return GetCard(CardType.Relic, 0, (int) pool);
        }

        cardRoll = Random.Range(0, GetCards((CardType) pool, tier).Length);
        CardInfo ret = GetCard((CardType) pool, tier, cardRoll);

        //Debug.Log("Roll: " + poolRoll.ToString() + ". Card: " + ret.cardName);
        return ret;
    }

    public CardPool GetPool(CardType type)
    {
        return cardPools[(int) type];
    }

    public CardTier[] GetTiers(CardType type)
    {
        return cardPools[(int) type].tiers;
    }

    public CardTier GetTier(CardType type, int tier)
    {
        return cardPools[(int) type].tiers[tier];
    }

    public CardInfo[] GetCards(CardType type, int tier)
    {
        return cardPools[(int) type].tiers[tier].cards;
    }

    public CardInfo GetCard(CardType type, int tier, int index)
    {
        return cardPools[(int) type].tiers[tier].cards[index];
    }
}
