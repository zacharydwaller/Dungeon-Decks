using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTooltip : Tooltip
{
    public Text nameText;
    public Text hpText;
    public Text damageText;

    public Transform auraPanel;

    Enemy enemy;

    private void Update()
    {
        if(enemy)
        {
            hpText.text = enemy.health.ToString("N2");

            //Update Auras
            for(int i = 0; i < enemy.maxAuras; i++)
            {
                AuraIcon icon = auraPanel.GetChild(i).GetComponent<AuraIcon>();

                if(icon.owner == null) icon.SetOwner(enemy);

                icon.UpdateIcon();
            }
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
        string ret;
        int damage =  enemy.info.magnitude;
        int duration = Mathf.Max(1, enemy.info.secondary);

        damage = (damage / duration);

        ret = damage.ToString();

        if(duration > 1)
        {
            ret = ret + "x" + duration.ToString();
        }

        return ret;
    }
}
