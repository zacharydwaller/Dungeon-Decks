using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/ClassDB/Database")]
public class ClassDatabase : ScriptableObject
{
    public ClassPool[] pools;

    public CharacterClass GetClass(StatType primary, StatType secondary)
    {
        return pools[(int) primary].classes[(int) secondary];
    }
}

[System.Serializable]
public class ClassPool
{
    public CharacterClass[] classes;
}