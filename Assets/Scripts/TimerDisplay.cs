using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{

    public TextMeshProUGUI seconds;
    public TextMeshProUGUI minutes;

    private float secondsCount = 0;
    private int minutesCount = 0;


    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    public void UpdateTimer()
    {

        secondsCount += Time.deltaTime;
        seconds.text = ((int)secondsCount).ToString();
        minutes.text = minutesCount.ToString();

        if (secondsCount >= 60)
        {
            secondsCount %= 60;
            minutesCount++;
        }


    }
}
