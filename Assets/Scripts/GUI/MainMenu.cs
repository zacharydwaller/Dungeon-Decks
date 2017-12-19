using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject characterCreationUI;
    public GameObject instructionsUI;
    public GameObject creditsUI;

    public GameObject[] instructionText;
    private int instructionIndex = 0;

    private void Start()
    {
        ShowMainMenu();

        instructionIndex = 1;
        ToggleMoreInstructions();
    }

    public void ShowMainMenu()
    {
        SwitchToPanel(mainMenuUI);
    }

    public void CharacterCreationUI()
    {
        SwitchToPanel(characterCreationUI);
    }

    public void ShowInstructions()
    {
        SwitchToPanel(instructionsUI);
    }

    public void ToggleMoreInstructions()
    {
        HideInstructionText();
        instructionIndex = (instructionIndex + 1) % instructionText.Length;
        instructionText[instructionIndex].SetActive(true);
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

    public void HideInstructionText()
    {
        foreach(var obj in instructionText)
        {
            obj.SetActive(false);
        }
    }

    public void SwitchToPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }
}
