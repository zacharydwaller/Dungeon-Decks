using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Database/Card")]
public class CardInfo : ScriptableObject
{
    public string cardName;
    public Color color;
    public Sprite image;
    public CardEffect effect;

    public bool isSelfCast { get { return effect.isSelfCast; } }

    public void DoEffect(GameObject user, Vector2 direction = default(Vector2))
    {
        effect.DoEffect(user, direction);
    }
}
