using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Database/Card")]
public class CardInfo : DBItem
{
    public enum BonusType
    {
        None, Strength, Magic, Alchemy
    }

    public string cardName;
    public Color color;
    public Sprite image;
    public CardEffect[] effects;
    public int[] magnitudes;
    public int[] secondaries;
    public string descOverride;
    public BonusType[] bonusTypes;
    public bool isConsumable;

    // Ranged or self cast cards with multiple effects must have ranged/selfcast effect first in effect list
    public bool isSelfCast { get { return effects[0].isSelfCast; } }
    public bool isRanged { get { return effects[0].isRanged; } }

    public string description
    {
        get
        {
            if(descOverride != string.Empty)
            {
                return ReplaceTokens(descOverride, 0);
            }
            else if(effects.Length == 1)
            {
                return EffectDesc(0);
            }
            else
            {
                string ret = string.Empty;

                for(int i = 0; i < effects.Length - 1; i++)
                {
                    ret = ret + EffectDesc(i) + "\n";
                }
                ret = ret + EffectDesc(effects.Length - 1);

                //ret.Trim('\n') not working

                return ret;
            }
        }
    }

    public void DoEffects(GameObject user, Vector2 direction = default(Vector2))
    {
        if(!effects[0].isSelfCast && direction != Vector2.zero)
        {
            user.GetComponent<Entity>().DoAttackAnimation(direction);
        }

        for(int i = 0; i < effects.Length; i++)
        {
            effects[i].DoEffect(user, direction, GetMagnitude(i), GetSecondary(i));
        }
    }

    private string EffectDesc(int index)
    {
        return ReplaceTokens(effects[index].rawDescription, index);
    }

    private string ReplaceTokens(string input, int index)
    {
        string ret = input;

        if(magnitudes.Length > index)
        {
            ret = ret.Replace("%m", GetMagnitude(index).ToString());
        }

        if(secondaries.Length > index)
        {
            ret = ret.Replace("%s", GetSecondary(index).ToString());
            ret = ret.Replace("%r", Mathf.Floor(GetMagnitude(index) / Mathf.Max(1, GetSecondary(index))).ToString());
        }

        return ret;
    }

    private int GetMagnitude(int index)
    {
        Player player = GameManager.singleton.player;

        if(player != null && bonusTypes.Length > index)
        {
            if(bonusTypes[index] == BonusType.None)
            {
                return magnitudes[index];
            }
            else if(bonusTypes[index] == BonusType.Magic)
            {
                return magnitudes[index] + player.magic;
            }
            else if(bonusTypes[index] == BonusType.Strength)
            {
                return magnitudes[index] + player.strength;
            }
            else if(bonusTypes[index] == BonusType.Alchemy)
            {
                return magnitudes[index] + player.alchemy;
            }
        }

        return magnitudes[index];
    }

    private int GetSecondary(int index)
    {
        if(index < secondaries.Length)
        {
            return secondaries[index];
        }
        else
        {
            return 0;
        }
    }
}
