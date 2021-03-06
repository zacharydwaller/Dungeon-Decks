﻿using UnityEngine;

[System.Serializable]
public class CardEffectSlot
{
    public CardEffect CardEffect;
    public float Magnitude;
    public int Duration;

    public Bonus[] Bonuses = new Bonus[]
    {
        new Bonus(StatType.Strength),
        new Bonus(StatType.Dexterity),
        new Bonus(StatType.Magic)
    };

    public string GetDescription()
    {
        if (CardEffect == null) return string.Empty;

        return CardEffect.MakeDescription(MagnitudeWithBonus(), Duration);
    }

    public void DoEffect(GameObject user, Vector2 direction = default)
    {
        if (CardEffect == null) return;

        var calculatedMagnitude = MagnitudeWithBonus();
        CardEffect.DoEffect(user, direction, calculatedMagnitude, Duration);
    }

    public float MagnitudeWithBonus()
    {
        var newMagnitude = Magnitude;
        Player player = GameManager.singleton?.player;

        if (player == null) return Magnitude;

        foreach(var bonus in Bonuses)
        {
            var statAmount = player.GetStat(bonus.StatType);
            newMagnitude += statAmount * bonus.Coefficient;
        }

        return newMagnitude;
    }
}

[System.Serializable]
public class Bonus
{
    public StatType StatType;
    public float Coefficient;

    public Bonus(StatType type)
    {
        StatType = type;
    }
}
