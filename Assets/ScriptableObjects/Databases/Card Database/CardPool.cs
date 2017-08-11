using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardDB/Pool")]
public class CardPool : ScriptableObject
{
    public CardTier[] tiers;
}

[System.Serializable]
public class CardTier
{
    public CardInfo[] cards;
}