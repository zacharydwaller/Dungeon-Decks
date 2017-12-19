using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject characterCreationUI;
    public GameObject instructionsUI;
    public GameObject creditsUI;

    public GameObject[] instructionText;
    private int instructionIndex = 0;

    public ClassDatabase classDB;
    public Image classSprite;
    public Text classNameText;
    public Text classStatsText;
    public Text classDescText;

    private void Start()
    {
        ShowMainMenu();

        instructionIndex = 1;
        ToggleMoreInstructions();

        UpdateClassDescription();
    }

    public void ShowMainMenu()
    {
        SwitchToPanel(mainMenuUI);
    }

    public void ShowCharacterCreationUI()
    {
        SwitchToPanel(characterCreationUI);
    }

    public void SelectPrimaryStat(string stat)
    {
        SettingsManager.GetScript.primaryStat = StatTypes.GetStat(stat);
        UpdateClassDescription();
    }

    public void SelectOffStat(string stat)
    {
        SettingsManager.GetScript.offStat = StatTypes.GetStat(stat);
        UpdateClassDescription();
    }

    public void UpdateClassDescription()
    {
        var settings = SettingsManager.GetScript;
        var cClass = classDB.GetClass(settings.primaryStat, settings.offStat);

        classSprite.sprite = cClass.sprite;
        classNameText.text = cClass.className;
        classStatsText.text = StatTypes.GetString(cClass.primaryStat) + "/" + StatTypes.GetString(cClass.offStat);
        classDescText.text = cClass.description;
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
        characterCreationUI.SetActive(false);
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
