using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ClassDatabase
{
    public static List<CharacterClass> CharacterClasses;

    static ClassDatabase()
    {
        var objectArray = Resources.LoadAll("Classes", typeof(CharacterClass));
        var objectList = new List<Object>(objectArray);

        CharacterClasses = objectList.Select(o => o as CharacterClass).ToList();
    }

    public static CharacterClass GetClass(StatType primary, StatType secondary)
    {
        return CharacterClasses.FirstOrDefault(c => c.primaryStat == primary && c.offStat == secondary);
    }
}