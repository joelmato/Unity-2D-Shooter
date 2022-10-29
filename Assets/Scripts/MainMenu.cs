using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject transitionAnimator;
    public GameObject controlsDisplay;

    void Start()
    {
        CursorController.instance.SetPointer();
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
