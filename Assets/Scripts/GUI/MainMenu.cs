using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject instructionsUI;


    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        instructionsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ShowInstructions()
    {
        instructionsUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
}
