﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Database/Card")]
public class CardInfo : DBItem
{
    public string cardName;
    public Color color;
    public Sprite image;
    public CardEffect[] effects;
    public int[] magnitudes;
    public string descOverride;
    public bool isConsumable;

    public bool isSelfCast { get { return effects[0].isSelfCast; } }

    public string description
    {
        get
        {
            if(descOverride != string.Empty)
            {
                return descOverride.Replace("%m", magnitudes[0].ToString());
            }
            else if(effects.Length == 1)
            {
                return EffectDesc(0);
            }
            else
            {
                string ret = string.Empty;

                for(int i = 0; i < effects.Length; i++)
                {
                    ret = ret + EffectDesc(i) + "\n";
                }
                ret.TrimEnd('\n');

                return ret;
            }
        }
    }

    public void DoEffects(GameObject user, Vector2 direction = default(Vector2))
    {
        for(int i = 0; i < effects.Length; i++)
        {
            effects[i].DoEffect(user, magnitudes[i], direction);
        }
    }

    private string EffectDesc(int index)
    {
        return effects[index].rawDescription.Replace("%m", magnitudes[index].ToString());
    }
}
