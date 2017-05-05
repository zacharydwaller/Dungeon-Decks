using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTooltip : Tooltip
{
    public Text nameText;
    public Text hpText;
    public Text damageText;

    Enemy enemy;

    private void Update()
    {
        if(enemy)
        {
            hpText.text = enemy.health.ToString();
        }
    }

    public override void SetItem(object newEnemy)
    {
        enemy = (Enemy) newEnemy;
        nameText.text = enemy.info.enemyName;
        hpText.text = enemy.health.ToString();
        damageText.text = enemy.info.attackEffect.GetEffectAmount().ToString();
    }
}
