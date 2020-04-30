using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Database/Card")]
public class CardInfo : DBItem
{
    public string cardName;
    public Color color;
    public Sprite image;
    public CardType cardType;
    public StatType[] statTypes = new StatType[2];
    public CardEffectSlot[] effectSlots = new CardEffectSlot[1];

    public string descOverride;
    
    public bool isConsumable;

    // Ranged or self cast cards with multiple effects must have ranged/selfcast effect first in effect list
    public bool isSelfCast { get { return effectSlots[0].CardEffect.isSelfCast; } }
    public bool isRanged { get { return effectSlots[0].CardEffect.isRanged; } }

    public string GetDescription()
    {
        // Return override if provided
        if(descOverride != string.Empty)
        {
            return descOverride;
        }
        // Otherwise return all descriptions concatenated
        else
        {
            string ret = string.Empty;

            for(int i = 0; i < effectSlots.Length; i++)
            {
                ret = ret + GetEffectSlot(i).GetDescription() + "\n";
            }

            return ret.Trim();
        }
    }

    public void DoEffects(GameObject user, Vector2 direction = default)
    {
        if(!GetEffect(0).isSelfCast && direction != Vector2.zero)
        {
            user.GetComponent<Entity>().DoAttackAnimation(direction);
        }

        foreach(var effectSlot in effectSlots)
        {
            effectSlot.DoEffect(user, direction);
        }

    }

    public CardEffectSlot GetEffectSlot(int index)
    {
        if (index >= effectSlots.Length) index = effectSlots.Length - 1;

        return effectSlots[index];
    }

    public CardEffect GetEffect(int index)
    {
        return GetEffectSlot(index).CardEffect;
    }
}