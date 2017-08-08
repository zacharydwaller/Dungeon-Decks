using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Enemy")]
public class EnemyInfo : DBItem
{
    public string enemyName;
    public Sprite sprite;
    public int maxHealth;
    public int damage;

    public int scoreValue { get { return maxHealth + damage; } }

    public CardEffect attackEffect;
}
