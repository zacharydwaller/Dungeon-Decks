using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Database/ClassDB/Class")]
public class CharacterClass : ScriptableObject
{
    public string className;
    public Sprite sprite;
    public StatType primaryStat;
    public StatType offStat;
    public string description;
}
