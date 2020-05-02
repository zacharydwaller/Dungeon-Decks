using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Enemy")]
public class EnemyInfo : DBItem
{
    public string enemyName;
    public int tier = 1;
    public Sprite sprite;
    public int maxHealth;
    public int magnitude;
    public int secondary;

    public int scoreValue { get { return maxHealth + magnitude; } }

    public CardEffect attackEffect;
}
