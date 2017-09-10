using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Strength, Magic, Dexterity, Enhancement
}

public class StatTypes
{
    public string strDesc
    {
        get
        {
            return "Strength boosts physical attack and armor cards. Strength also allows you to block more damage with AP";
        }
    }

    public string magDesc
    {
        get
        {
            return "Magic powers up offensive spells and increases the power of potion cards.";
        }
    }

    public string dexDesc
    {
        get
        {
            return "Dexterity increases the power of ranged weapon cards, cards with Bleed damage such as Daggers, and poison cards. Dexterity also allows you to save AP when it blocks damage.";
        }
    }

    public string enhDesc
    {
        get
        {
            return "Enhancement powers up defensive spell cards such as heals and buffs. Enhancement also gives you straight damage reduction, applied to the damage that AP did not block.";
        }
    }
}