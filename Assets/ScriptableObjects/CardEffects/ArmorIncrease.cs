using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/ArmorIncrease")]
public class ArmorIncrease : CardEffect
{
    public int amount;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2))
    {
        user.GetComponent<Player>().armor += amount;
    }
}
