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
        if(enemy)
        {
            nameText.text = enemy.info.enemyName;
            hpText.text = enemy.health.ToString();
            damageText.text = GetDamageText();
        }
    }

    public string GetDamageText()
    {
        Player player = GameManager.singleton.player;
        string ret;
        int damage =  enemy.info.magnitude;
        int duration = Mathf.Max(1, enemy.info.secondary);
        int dr = 0;

        if(player)
        {
            dr = player.dmgReduction;
        }

        damage = (damage / duration) - dr;

        ret = damage.ToString();

        if(duration > 1)
        {
            ret = ret + "x" + duration.ToString();
        }

        return ret;
    }
}
