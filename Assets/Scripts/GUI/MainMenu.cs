using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject instructionsUI;
    public GameObject creditsUI;


    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        SwitchToPanel(mainMenuUI);
    }

    public void ShowInstructions()
    {
        SwitchToPanel(instructionsUI);
    }

    public void ShowCredits()
    {
        SwitchToPanel(creditsUI);
    }

    public void HideAllPanels()
    {
        mainMenuUI.SetActive(false);
        instructionsUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void SwitchToPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }
}
