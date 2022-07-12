using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedMeter : MonoBehaviour
{
    [SerializeField] private Transform needleTransform;
    [SerializeField] private Text currentSpeedText;
    [SerializeField] private Text currentGearText;
    [SerializeField] private Text currentRPMText;
    private const int MAX_SPEED_ANGLE = -20;
    private const int ZERO_SPEED_ANGLE = 210;
    private float rpmMax;

    public void SetMaxRPM(float rpmMax)
    {
        this.rpmMax = rpmMax;
    }

    public void SetNeedleAngle(float currentRPM,int currentGearIndex,float speed)
    {
        var angle = GetSpeedRotation(currentRPM);
        needleTransform.eulerAngles = new Vector3(0,0,angle);
        currentSpeedText.text = String.Format("{0:0}", speed);
        currentGearText.text ="GEAR:"+ currentGearIndex.ToString();
        currentRPMText.text ="RPM:"+ String.Format("{0:0.0}", currentRPM);
    }
    private float GetSpeedRotation(float RPM)
    {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;
        float rpmNormalized = RPM / rpmMax;
        return ZERO_SPEED_ANGLE - (rpmNormalized * totalAngleSize);
    }
}
