using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject transitionAnimator;
    public GameObject controlsDisplay;
    public GameObject settings;
    public TMP_Dropdown resolutionDropdown;

    void Start()
    {
        CursorController.instance.SetPointer();

    }
    public void StartGame()
    {
        // Loads the main game scene
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene("Main"));

        Time.timeScale = 1.0f;
    }

    public void ShowControls()
    {
        gameObject.SetActive(false); // Hides the main menu
        controlsDisplay.SetActive(true); // Shows the game controls screen
    }

    public void ShowSettings()
    {
        gameObject.SetActive(false); // Hides the main menu
        settings.SetActive(true); // Shows the settings screen
    }

    public void Back()
    {
        gameObject.SetActive(true); // Shows the main menu
        controlsDisplay.SetActive(false); // Hides the game controls screen
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
