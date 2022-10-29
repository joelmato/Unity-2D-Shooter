using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsDisplay;
    public Player player;
    public CanvasGroup gameUI;


    public GameObject transitionAnimator;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !(player.currentHealth == 0))
        {
            if (isPaused)
            {
                Continue();
            } 
            else
            {
                Pause();
            }
        }
    }

    public void Continue()
    {
        pauseMenuUI.SetActive(false);
        controlsDisplay.SetActive(false);
        CursorController.instance.SetCrosshair();
        gameUI.alpha = 1.0f;
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        CursorController.instance.SetPointer();
        gameUI.alpha = 0.5f;
        Time.timeScale = 0f;
        isPaused = true;    
    }

    public void Restart()
    {
        player.currentHealth = player.maxHealth;

        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene(SceneManager.GetActiveScene().name));
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Quit()
    {
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene("MainMenu"));
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void ShowControls()
    {
        controlsDisplay.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        controlsDisplay.SetActive(false);
    }

    public bool GetPausedStatus()
    {
        return isPaused;
    }
}
