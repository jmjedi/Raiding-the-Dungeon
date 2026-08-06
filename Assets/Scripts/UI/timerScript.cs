using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class timerScript : MonoBehaviour
{
    bool stopwatchActive = true;
    float currentTime;
    public Text currentTimeText;
    void Start()
    {
        stopwatchActive = true;

        currentTime = 0;

    }

    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        PlayerPrefs.SetFloat("Seconds", time.Seconds);
        PlayerPrefs.SetFloat("Minuets", time.Minutes);
        currentTimeText.text = time.Minutes.ToString() + ": " + time.Seconds.ToString();
        PlayerPrefs.SetString("Time", currentTimeText.text);
    }

  public void StartStopwatch()
  {
      stopwatchActive = true;
  }


  public void StopStopwatch()
  {
      stopwatchActive = false;
  }

}
