using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GearController
{
    public List<Gear> GearList = new List<Gear>();
    private CarController carController;

    
    private float shiftingThreshold; 
    private int currentGearIndex;
    private float currentRPM;
    

    public GearController(CarController carController)
    {
        this.carController = carController;
        Initialize();
    }

    public void Initialize()
    {
        float initialGearRatio = 0.1875f;
        for (int i = 1; i < 6; i++)
        {
            GearList.Add(new Gear(i, initialGearRatio));
            initialGearRatio *= 1.6f;
        }
        shiftingThreshold = carController.MaxSpeed / GearList.Count;
    }
    
    public float GetLastGearCircumference()
    {
        var lastIndexOfGearList = GearList.Count - 1;
        return GearList[lastIndexOfGearList].GearCircumference;
    }



    public void SetGear(float currentSpeed,bool IsAccelerating)
    {
        currentRPM = CalculateRPM(currentSpeed,IsAccelerating);
        var kmh =  currentSpeed * 60 * 60 / 1000;
        UIController.Instance.SetNeedleAngle(currentRPM,currentGearIndex,kmh);
    }
    private float CalculateRPM(float roadPerSec,bool IsAccelerating)
    {
        var activeGear = GetActiveGear(roadPerSec,IsAccelerating);

        var RPM = roadPerSec / activeGear.GearCircumference / carController.RpmFactor;
        RPM = Mathf.Clamp(RPM,0,carController.MaxRPM);
        
        return RPM;
    }
    private Gear GetActiveGear(float roadPerSec,bool IsAccelerating)
    {
        if (IsAccelerating)
            currentGearIndex = (int)(roadPerSec / shiftingThreshold) + 1;

        currentGearIndex = Mathf.Clamp(currentGearIndex, 1, GearList.Count);
        var activeGear = GearList.Find(x => x.GearNo == currentGearIndex);

        return activeGear;
    }
   

}