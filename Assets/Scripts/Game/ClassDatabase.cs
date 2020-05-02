using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ClassDatabase
{
    public static List<CharacterClass> CharacterClasses;

    static ClassDatabase()
    {
        var ccArray = Resources.LoadAll("Classes", typeof(CharacterClass)).Cast<CharacterClass>();
        CharacterClasses = ccArray.ToList();
    }

    public static CharacterClass GetClass(StatType primary, StatType secondary)
    {
        return CharacterClasses.FirstOrDefault(c => c.primaryStat == primary && c.offStat == secondary);
    }
}