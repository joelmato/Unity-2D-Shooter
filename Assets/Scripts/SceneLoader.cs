using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    
    public IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start"); // Playes the scene transition animation

        // Loads the scene with sceneName after 1 second has passed
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName); 
    }

}
