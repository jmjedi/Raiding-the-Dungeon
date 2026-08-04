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
        currentTimeText.text = time.Minutes.ToString() + ": " + time.Seconds.ToString();
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
