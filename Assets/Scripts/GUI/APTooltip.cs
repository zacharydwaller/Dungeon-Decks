using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APTooltip : Tooltip
{
    public override void SetItem(object newItem = null)
    {
        Player player = GameManager.singleton.player;
    }
}
