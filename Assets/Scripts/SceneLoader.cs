using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public bool canPauseGame = true;

    private void Start()
    {
        StartCoroutine(DisablePausing());
    }

    public IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start"); // Playes the scene transition animation
        canPauseGame = false;
        // Loads the scene with sceneName after 1 second has passed
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);

    }

    IEnumerator DisablePausing()
    {
        canPauseGame = false;
        yield return new WaitForSeconds(1);
        canPauseGame = true;
    }

}
