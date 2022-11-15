using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingProgressBar : MonoBehaviour
{

    public Image progressBarFill;

    private float timeMax;
    private float timeLeft;
    private bool runTimer = false;


    
    void Update()
    {
        if (timeLeft > 0 && runTimer)
        {
            timeLeft -= Time.deltaTime; 
            progressBarFill.fillAmount = timeLeft / timeMax;
            if (timeLeft <= 0) StopTimer();
        }
    }

    public void StartTimer(float newTimeMax)
    {
        gameObject.SetActive(true); // Shows the timer bar
        timeMax = newTimeMax; 
        timeLeft = timeMax; 
        runTimer = true;

    }

    private void StopTimer()
    {
        runTimer = false;
        gameObject.SetActive(false); // Hides the timer bar
    }
}
