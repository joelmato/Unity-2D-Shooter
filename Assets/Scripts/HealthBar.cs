using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Method for setting the max value of the HealthBar slider
    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    // Method for setting the value of the HealthBar slider
    public void SetHealth(int newHealth) 
    {
        slider.value = newHealth;
    }

}
