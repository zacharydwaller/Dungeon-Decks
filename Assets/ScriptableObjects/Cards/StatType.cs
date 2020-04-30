using System.Collections.Generic;

public enum StatType
{
    Strength,
    Magic,
    Dexterity,
    Enhancement,
    NoStat
}

public static class StatTypes
{
    public static string strDesc
    {
        get
        {
            return "Strength boosts physical attack and armor cards. Strength also allows you to block more damage with AP";
        }
    }

    public static string magDesc
    {
        get
        {
            return "Magic powers up offensive spells and increases the power of potion cards.";
        }
    }

    public static string dexDesc
    {
        get
        {
            return "Dexterity increases the power of ranged weapon cards, cards with Bleed damage such as Daggers, and poison cards. Dexterity also allows you to save AP when it blocks damage.";
        }
    }

    public static string enhDesc
    {
        get
        {
            return "Enhancement powers up defensive spell cards such as heals and buffs. Enhancement also gives you straight damage reduction, applied to the damage that AP did not block.";
        }
    }

    public static List<StatType> GetOtherStats(IEnumerable<StatType> primaryStats)
    {
        var list = new List<StatType>();
        for(int i = 0; i < 4; i++)
        {
            list.Add((StatType) i);
        }

        foreach(var stat in primaryStats)
        {
            list.Remove(stat);
        }

        return list;
    }

    public static StatType GetStat(string statName)
    {
        var comp = System.StringComparer.Create(System.Globalization.CultureInfo.InvariantCulture, true);

        if(comp.Compare(statName, "Strength") == 0
            || comp.Compare(statName, "Str") == 0)
        {
            return StatType.Strength;
        }
        if(comp.Compare(statName, "Magic") == 0
            || comp.Compare(statName, "Mag") == 0)
        {
            return StatType.Magic;
        }
        if(comp.Compare(statName, "Dexterity") == 0
            || comp.Compare(statName, "Dex") == 0)
        {
            return StatType.Dexterity;
        }
        if(comp.Compare(statName, "Enhancement") == 0
            || comp.Compare(statName, "Enhance") == 0
            || comp.Compare(statName, "Enh") == 0)
        {
            return StatType.Enhancement;
        }

        return StatType.Strength;
    }
}