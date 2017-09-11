using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardDB/Database")]
public class CardDatabase : ScriptableObject
{
    public CardPool primaryPool;
    public CardPool secondaryPool;
    public CardPool mixedPool;
    public CardPool potionPool;
    public CardPool relicPool;

    public CardPool[] statPools;



    /* Gets a card of a selected tier
     * Card Choice is weighted by item type and main/off stat
     * Spec:        70%
     * Potions:     20%
     * Relics:      10%
     * 
     * Type of Spec card chosen is weighted 45/35/20 for Primary/Secondary/Mixed Decks
     * Type of Relic chosen is weighted 50/40/10 for Primary/Secondary/Other stats
     */
    public CardInfo GetCardOfTier(int tier)
    {
        Player player = GameManager.singleton.player;
        int poolRoll, cardRoll;
        CardPool pool;

        tier = Mathf.Min(tier, statPools[0].tiers.Length - 1);

        poolRoll = Random.Range(0, 100);

        // Spec
        if(poolRoll < 70)
        {
            poolRoll = Random.Range(0, 100);

            // Primary
            if(poolRoll < 45)
            {
                pool = primaryPool;
            }
            else if(poolRoll < 80)
            {
                pool = secondaryPool;
            }
            else
            {
                pool = mixedPool;
            }
        }
        // Potion
        else if(poolRoll < 90)
        {
            pool = potionPool;
        }
        // Relic
        else
        {
            StatType stat;
            poolRoll = Random.Range(0, 100);
            pool = relicPool;

            // Main Stat
            if(poolRoll < 50)
            {
               stat = player.primaryStats[0];
            }
            // Off Stat
            else if(poolRoll < 90)
            {
                stat = player.primaryStats[1];
            }
            // Other Stat 1
            else if(poolRoll < 95)
            {
                stat = player.otherStats[0];
            }
            // Other Stat 2
            else
            {
                stat = player.otherStats[1];
            }

            return relicPool.GetCard(0, (int) stat);
        }

        cardRoll = Random.Range(0, pool.NumCards(tier));
        CardInfo ret = pool.GetCard(tier, cardRoll);

        Debug.Log("Roll: " + poolRoll.ToString() + ". Card: " + ret.cardName);
        return ret;
    }

    public void LoadSpecPool(StatType stat1, StatType stat2)
    {
        primaryPool = statPools[(int) stat1];
        secondaryPool = statPools[(int) stat2];

        // StrMag
        if((stat1 == StatType.Strength && stat2 == StatType.Magic) ||
           (stat2 == StatType.Strength && stat1 == StatType.Magic))
        {
            mixedPool = statPools[(int) CardPool.Type.StrMag];
        }
        // StrDex
        if((stat1 == StatType.Strength && stat2 == StatType.Dexterity) ||
           (stat2 == StatType.Strength && stat1 == StatType.Dexterity))
        {
            mixedPool = statPools[(int) CardPool.Type.StrDex];
        }
        // StrEnh
        if((stat1 == StatType.Strength && stat2 == StatType.Enhancement) ||
           (stat2 == StatType.Strength && stat1 == StatType.Enhancement))
        {
            mixedPool = statPools[(int) CardPool.Type.StrEnh];
        }
        // MagDex
        if((stat1 == StatType.Magic && stat2 == StatType.Dexterity) ||
           (stat2 == StatType.Magic && stat1 == StatType.Dexterity))
        {
            mixedPool = statPools[(int) CardPool.Type.MagDex];
        }
        // MagEnh
        if((stat1 == StatType.Magic && stat2 == StatType.Enhancement) ||
           (stat2 == StatType.Magic && stat1 == StatType.Enhancement))
        {
            mixedPool = statPools[(int) CardPool.Type.MagEnh];
        }
        // DexEnh
        if((stat1 == StatType.Dexterity && stat2 == StatType.Enhancement) ||
           (stat2 == StatType.Dexterity && stat1 == StatType.Enhancement))
        {
            mixedPool = statPools[(int) CardPool.Type.DexEnh];
        }
    }
}
