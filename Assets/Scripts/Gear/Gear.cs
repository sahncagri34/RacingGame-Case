using UnityEngine;

[System.Serializable]
public class Gear
{
    public int GearNo;
    public float Ratio;
    public float GearCircumference;

    public Gear(int _gearNo,float _ratio)
    {
        GearNo = _gearNo;
        Ratio  = _ratio;
        GearCircumference = 2 * Mathf.PI * Ratio;
    }

}