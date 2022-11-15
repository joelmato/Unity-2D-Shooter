using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settings;
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
        pauseMenuUI.SetActive(false); // Hides the pause menu
        controlsDisplay.SetActive(false); // Hides the game controls screen
        settings.SetActive(false); // Hides the settings screen

        CursorController.instance.SetCrosshair();
        gameUI.alpha = 1.0f;
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Shows the pause screen

        CursorController.instance.SetPointer();
        gameUI.alpha = 0.5f;
        Time.timeScale = 0f;
        isPaused = true;    
    }

    public void Restart()
    {
        player.currentHealth = player.maxHealth;

        // Reloads the same scene in order to restart the game
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene(SceneManager.GetActiveScene().name));

        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Quit()
    {
        // Loads the main menu scene
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene("MainMenu"));

        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void ShowControls()
    {
        pauseMenuUI.SetActive(false); // Hides the pause screen
        controlsDisplay.SetActive(true); // Shows the game controls screen
    }
    public void ShowSettings()
    {
        pauseMenuUI.SetActive(false); // Hides the pause screen
        settings.SetActive(true); // Shows the settings screen
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true); // Shows the pause screen
        controlsDisplay.SetActive(false); // Hides the game controls screen
    }
}
