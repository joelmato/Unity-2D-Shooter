using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public Player player;
    public CanvasGroup gameUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
        gameUI.alpha = 1.0f;
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameUI.alpha = 0.5f;
        Time.timeScale = 0f;
        isPaused = true;    
    }

    public void Restart()
    {
        player.currentHealth = player.maxHealth;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f; 
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
