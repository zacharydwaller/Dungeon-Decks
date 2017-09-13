﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPanel
{
    PlayerUI ui;
    Player player;
    Transform panel;
    GameObject auraIconRef;

    public AuraPanel(PlayerUI newUI)
    {
        ui = newUI;
        player = ui.player;
        panel = ui.auraPanelObj.transform;
        auraIconRef = ui.auraIconRef;
    }

    public void Update()
    {
        if(!player) return;

        // Update each icon
        for(int i = 0; i < player.maxAuras; i++)
        {
            GetAuraIcon(i).UpdateIcon();
        }
    }

    public void SetPlayer(Player newPlayer)
    {
        player = newPlayer;
    }

    public AuraIcon GetAuraIcon(int index)
    {
        if(index >= panel.childCount)
        {
            return null;
        }
        else
        {
            return panel.GetChild(index).GetComponent<AuraIcon>();
        }
    }
}
