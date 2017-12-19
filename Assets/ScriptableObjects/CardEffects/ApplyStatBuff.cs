using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/CardEffect/ApplyStatBuff")]
public class ApplyStatBuff : CardEffect
{
    public StatType stat;
    public Sprite icon;

    public override void DoEffect(GameObject user, Vector2 direction = default(Vector2), float magnitude = 0, int duration = 0)
    {
        Entity ent = user.GetComponent<Entity>();
        Aura aura = new Aura(ent, CreateBuff(), magnitude, duration);

        ent.ApplyAura(aura);
    }

    private Buff CreateBuff()
    {
        Buff buff = Buff.CreateInstance(stat);
        buff.icon = icon;
        buff.tooltipDescription = rawDescription.Replace("%s", "%d");
        return buff;
    }
}
