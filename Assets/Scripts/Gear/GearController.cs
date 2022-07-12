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




    public void SetGear(float currentSpeed)
    {
        currentRPM = CalculateRPM(currentSpeed);
        var kmh =  currentSpeed * 60 * 60 / 1000;
        UIController.Instance.SetNeedleAngle(currentRPM,currentGearIndex,kmh);
    }
    private float CalculateRPM(float roadPerSec)
    {
        currentGearIndex = (int)(roadPerSec / shiftingThreshold) + 1;
        currentGearIndex = Mathf.Clamp(currentGearIndex, 1, GearList.Count);

        var activeGear = GearList.Find(x=> x.GearNo == currentGearIndex);
        var RPM = roadPerSec / activeGear.GearCircumference / 5;
        RPM = Mathf.Clamp(RPM,0,carController.MaxRPM);
        return RPM;
    }

}