using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject transitionAnimator;
    public Player player;
    public Shooting shootingScript;
    public GameObject gameOverUI;
    public CanvasGroup gameUI;
    public TimerDisplay timerDisplay;

    public TextMeshProUGUI timeSurvivedForText;

    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0 && !gameOver)
        {

            gameOverUI.SetActive(true); // Displays the game over screen
            gameUI.alpha = 0.5f;
            Time.timeScale = 0f;

            shootingScript.canShoot = false;
            shootingScript.isReloading = true;
            gameOver = true;
            timeSurvivedForText.text = timerDisplay.GetMinutes() + ":" + timerDisplay.GetSeconds();

        }
    }

    public void Retry()
    {
        player.currentHealth = player.maxHealth;

        // Reloads the same scene in order to restart the game
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene(SceneManager.GetActiveScene().name));

        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        // Loads the main menu scene
        StartCoroutine(transitionAnimator.GetComponent<SceneLoader>().LoadScene("MainMenu"));

        Time.timeScale = 1.0f;
    }
}
