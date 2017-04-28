using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    public enum Target
    {
        Melee, Ranged, Self
    };

    public enum TargetStat
    {
        Health, Armor
    }

    public string name;
    public Color color;
    public Sprite image;
    public Target target;
    public TargetStat targetStat;
    public int effectAmount;
}
