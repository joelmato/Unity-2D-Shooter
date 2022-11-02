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

    Resolution[] screenResolutions;

    void Start()
    {
        CursorController.instance.SetPointer();
        Time.timeScale = 1.0f;

    }
    public void StartGame()
    {
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene("Main"));
        //SceneManager.LoadScene("Main");
        Time.timeScale = 1.0f;
    }

    public void ShowControls()
    {
        gameObject.SetActive(false);
        controlsDisplay.SetActive(true);
    }

    public void ShowSettings()
    {
        gameObject.SetActive(false);
        settings.SetActive(true);
    }

    public void Back()
    {
        gameObject.SetActive(true);
        controlsDisplay.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
