using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{


    [SerializeField] private Text notificationText;
    [SerializeField] private SpeedMeter speedMeter;
    [SerializeField] private Text stopwatch;

    public void ShowNotification(string data)
    {
        notificationText.text = data;
    }

    public void SetMaxRPMOnSpeedMeter(float rpmMax)
    {
        speedMeter.SetMaxRPM(rpmMax);
    }
    public void ShowElapsedTime(float elapsedTime)
    {
       stopwatch.text = String.Format("{0:0.00}", elapsedTime);
    }


    public void SetNeedleAngle(float currentRPM, int currentGearIndex, float currentSpeed)
    {
        speedMeter.SetNeedleAngle(currentRPM, currentGearIndex, currentSpeed);
    }
}
