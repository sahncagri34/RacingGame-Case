using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputGetter
{
    public float Horizontal{
        get;
    }
    public float Vertical{
        get;
    }
    public bool IsAccelerating
    {
        get;
    }
}
