using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APTooltip : Tooltip
{
    public Text bbText;
    public Text bbStrText;
    public Text bbMagText;
    public Text bbDexText;
    public Text bbEnhText;

    public Text apText;
    public Text apStrText;
    public Text apMagText;
    public Text apDexText;
    public Text apEnhText;

    public override void SetItem(object newItem = null)
    {
        Player player = GameManager.singleton.player;

        bbText.text = player.bonusDmgBlock.ToString();
        bbStrText.text = (player.strength / 2).ToString();
        bbMagText.text = (player.magic / 4).ToString();
        bbDexText.text = (player.dexterity / 4).ToString();
        bbEnhText.text = (player.enhancement / 2).ToString();

        apText.text = player.bonusAPSave.ToString();
        apStrText.text = (player.strength / 4).ToString();
        apMagText.text = (player.magic / 2).ToString();
        apDexText.text = (player.dexterity / 2).ToString();
        apEnhText.text = (player.enhancement / 4).ToString();
    }
}
