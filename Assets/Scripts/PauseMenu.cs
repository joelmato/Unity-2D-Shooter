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

        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene(SceneManager.GetActiveScene().name));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f; 
    }

    public void Quit()
    {
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene("MainMenu"));
        //SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
