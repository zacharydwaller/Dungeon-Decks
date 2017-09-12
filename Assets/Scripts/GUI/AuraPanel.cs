using System.Collections;
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

        int auraCount = player.auraCount;
        int iconCount = panel.transform.childCount;

        // Make sure there exists the right number of icons
        if(auraCount > iconCount)
        {
            for(int i = iconCount; i < auraCount; i++)
            {
                CreateAuraIcon(i);
            }
        }
        else if(iconCount > auraCount)
        {
            for(int i = iconCount - 1; i >= auraCount; i--)
            {
                DestroyAuraIcon(i);
            }
        }

        // Update each icon
        for(int i = 0; i < auraCount; i++)
        {
            GetAuraIcon(i).UpdateIcon();
        }
    }

    public GameObject CreateAuraIcon(int index)
    {
        GameObject icon = GameObject.Instantiate(auraIconRef);
        icon.GetComponent<AuraIcon>();

        icon.transform.SetParent(panel.transform);

        return icon;
    }

    public void DestroyAuraIcon(int index)
    {
        GameObject.Destroy(panel.GetChild(index).gameObject);
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
