using System.Collections.Generic;

public enum StatType
{
    Strength,
    Magic,
    Dexterity,
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

        return StatType.NoStat;
    }
}