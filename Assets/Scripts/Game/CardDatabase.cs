using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardDatabase
{
    public static List<CardInfo> CorePool;
    public static List<CardInfo> ConsumablePool;
    public static List<CardInfo> RelicPool;

    public const int CoreChance = 70;
    public const int ConsumableChance = 25;

    public const int CorePrimaryChance = 60;

    public const int ConsumablePrimaryChance = 60;

    public const int RelicPrimaryChance = 60;
    public const int RelicSecondaryChance = 30;

    static CardDatabase()
    {
        CorePool = Resources.LoadAll("Cards/Core", typeof(CardInfo)).Cast<CardInfo>().ToList();
        ConsumablePool = Resources.LoadAll("Cards/Consumable", typeof(CardInfo)).Cast<CardInfo>().ToList();
        RelicPool = Resources.LoadAll("Cards/Relic", typeof(CardInfo)).Cast<CardInfo>().ToList();
    }

    /* 
     * Gets a card of a selected tier
     * Card Choice is weighted by item type and main/off stat
     * Core: 65%
     * Consumable: 25%
     * Relics: 10%
     * 
     * Type of Spec card chosen is weighted 60/40 for Primary/Secondary Decks
     * Type of Relic chosen is weighted 60/30/10 for Primary/Secondary/Mixed stats
     */
    public static CardInfo GetCardOfStatTier(StatType primary, StatType secondary, int tier)
    {
        Player player = GameManager.singleton.player;
        List<CardInfo> pool;
        StatType stat;

        int poolRoll = Random.Range(0, 100);
        int statRoll = Random.Range(0, 100);

        // Core
        if (poolRoll < CoreChance)
        {
            pool = CorePool;

            if(statRoll < CorePrimaryChance) stat = primary;
            else stat = secondary;
        }
        // Consumable
        else if(poolRoll < CoreChance + ConsumableChance)
        {
            pool = ConsumablePool;

            if (statRoll < ConsumablePrimaryChance) stat = primary;
            else stat = secondary;
        }
        // Relic
        else
        {
            pool = RelicPool;

            if (statRoll < RelicPrimaryChance) stat = primary;
            else if (statRoll < RelicPrimaryChance + RelicSecondaryChance) stat = secondary;
            else stat = StatType.NoStat;
        }

        var cardInfo = GetRandomCard(pool, stat, tier);

        Debug.Log($"PoolRoll: {poolRoll}; StatRoll: {statRoll}; Tier: {tier}; Card: {cardInfo?.cardName}");

        // If GetRandomCard couldn't find a card, roll again
        if(cardInfo == null)
        {
            Debug.Log("Couldn't find card! Rerolling.");
            cardInfo = GetCardOfStatTier(primary, secondary, tier);
        }

        return cardInfo;
    }

    public static CardInfo GetRandomCard(List<CardInfo> pool, StatType stat, int tier)
    {
        // If selecting relic, ignore tier and just pick one by stat
        if(pool.Equals(RelicPool))
        {
            if (stat != StatType.NoStat) pool = pool.Where(c => c.HasStat(stat)).ToList();
            // if NoStat, then select a mixed relic
            else pool = pool.Where(c => c.StatTiers.Length > 1).ToList();
        }
        else
        {
            do
            {
                pool = pool.Where(c => c.IsInStatTier(stat, tier)).ToList();

                // May have to lower tier
                tier--;
            } while (pool.Count == 0 && tier > 0);
        }

        // Return null in case we can't find a card
        if(pool.Count == 0)
        {
            return null;
        }

        int cardRoll = Random.Range(0, pool.Count);
        return pool[cardRoll];
    }
}
