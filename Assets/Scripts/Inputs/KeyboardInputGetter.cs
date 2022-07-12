using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputGetter : MonoBehaviour, IInputGetter
{
    public float Horizontal { get; private set; }
    public float Vertical
    {
        get
        {
            return _vertical;
        }
        private set
        {
            IsAccelerating = value > 0;
            _vertical = value;
        }
    }
    public bool IsAccelerating { get; private set; }

    private float _vertical;

    private void Update() => GetInput();

    private void GetInput()
    {
        Vertical = Input.GetAxisRaw("Vertical");
    }
}
