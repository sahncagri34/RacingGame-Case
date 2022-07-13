using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputGetter : MonoBehaviour, IInputGetter
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public bool IsAccelerating { get; private set; }

    private void Update() => GetInput();

    /// <summary>
    /// KeyCode.Space => brake
    /// </summary>
    private void GetInput()
    {
        Vertical = Input.GetAxisRaw("Vertical");
        IsAccelerating = Vertical > 0 && !Input.GetKey(KeyCode.Space);
    }
}
