using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public bool isSelfCast;
    public string description;

    public abstract void DoEffect(GameObject user, Vector2 direction = default(Vector2));
}
