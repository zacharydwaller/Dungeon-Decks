using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/StatIncrease")]
public class StatIncrease : CardEffect
{
    public enum Stat
    {
        HP, AP, Str, Mag, Dex, Enh
    }

    public Stat stat;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2), int amount = 0, int ignore = 0)
    {
        switch(stat)
        {
            case Stat.HP:
                user.GetComponent<Player>().health += amount;
                break;
            case Stat.AP:
                user.GetComponent<Player>().armor += amount;
                break;
            case Stat.Str:
                user.GetComponent<Player>().strength += amount;
                break;
            case Stat.Mag:
                user.GetComponent<Player>().magic += amount;
                break;
            case Stat.Dex:
                user.GetComponent<Player>().dexterity += amount;
                break;
            case Stat.Enh:
                user.GetComponent<Player>().enhancement += amount;
                break;
        }
        
    }
}
