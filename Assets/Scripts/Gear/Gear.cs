using UnityEngine;

[System.Serializable]
public class Gear
{
    public int GearNo;
    public float GearCircumference;

    public Gear(int _gearNo,float _ratio)
    {
        GearNo = _gearNo;
        GearCircumference = 2 * Mathf.PI * _ratio;
    }

}