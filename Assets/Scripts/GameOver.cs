using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject transitionAnimator;
    public Player player;
    public Shooting shootingScript;
    public GameObject gameOverUI;
    public CanvasGroup gameUI;

    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (player.currentHealth <= 0 && !gameOver)
        {

            gameOverUI.SetActive(true);
            gameUI.alpha = 0.5f;
            Time.timeScale = 0f;
            shootingScript.canShoot = false;
            shootingScript.isReloading = true;
            gameOver = true;

        }
    }

    public void Retry()
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
