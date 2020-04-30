using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardDB/Pool")]
public class CardPool : ScriptableObject
{
    public enum Type
    {
        Str, Mag, Dex, Enh, StrMag, StrDex, StrEnh, MagDex, MagEnh, DexEnh, Potion, Relic
    }

    public CardTier[] tiers;

    public int NumCards(int tier)
    {
        return tiers[tier].cards.Length;
    }

    public CardInfo GetCard(int tier, int index)
    {
        if (tier >= tiers.Length) tier = tiers.Length - 1;
        if (index >= NumCards(tier)) index = NumCards(tier) - 1;

        return tiers[tier].cards[index];
    }
}

[System.Serializable]
public class CardTier
{
    public CardInfo[] cards;
}