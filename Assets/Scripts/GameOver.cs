using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public Player player;
    public GameObject gameOverUI;
    public CanvasGroup gameUI;

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0)
        {

            gameOverUI.SetActive(true);
            gameUI.alpha = 0.5f;
            Time.timeScale = 0f;
        }
    }

    public void Retry()
    {
        player.currentHealth = player.maxHealth;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
