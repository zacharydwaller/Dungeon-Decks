using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Enemy")]
public class EnemyInfo : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;
    public int maxHealth;
    public int scoreValue;

    public CardEffect attackEffect;
}
