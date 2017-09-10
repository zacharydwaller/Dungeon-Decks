using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APTooltip : Tooltip
{
    public Text blockText;
    public Text saveText;
    public Text drText;
    public Text potionText;

    public override void SetItem(object newItem = null)
    {
        Player player = GameManager.singleton.player;

        blockText.text = "+" + player.bonusDmgBlock;
        saveText.text = "+" + player.bonusAPSave;
        drText.text = "+" + player.bonusDR;
        potionText.text = "+" + player.bonusPotion;
    }
}
