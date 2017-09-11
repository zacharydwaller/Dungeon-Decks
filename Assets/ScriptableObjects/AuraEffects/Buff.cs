﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/Buff")]
public class Buff : AuraEffect
{
    /*
     * New Tooltip Description Tag
     * %stat - Stat buffed
     */
    public StatType stat;

    public static Buff CreateInstance(StatType newStat)
    {
        Buff ret = CreateInstance<Buff>();
        ret.stat = newStat;
        return ret;
    }

    public override void Tick() { }

    public override void OnAdd()
    {
        Player player = (Player) aura.owner;

        player.ModifyStat(stat, aura.magnitude);
    }

    public override void OnRemove()
    {
        Player player = (Player) aura.owner;

        player.ModifyStat(stat, -aura.magnitude);
    }

    public override string GetTooltip()
    {
        string ret = base.GetTooltip();

        ret = ret.Replace("%stat", stat.ToString());

        return ret;
    }
}
