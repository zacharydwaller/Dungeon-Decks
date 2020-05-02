using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/StatIncrease")]
public class StatIncrease : CardEffect
{
    public enum Stat
    {
        HP, AP, Str, Mag, Dex
    }

    public Stat stat;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2), float amount = 0, int ignore = 0)
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
                user.GetComponent<Player>().strength += (int) amount;
                break;
            case Stat.Mag:
                user.GetComponent<Player>().magic += (int) amount;
                break;
            case Stat.Dex:
                user.GetComponent<Player>().dexterity += (int) amount;
                break;
        }
        
    }
}
