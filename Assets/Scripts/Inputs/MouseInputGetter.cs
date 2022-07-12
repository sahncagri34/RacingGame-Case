using UnityEngine;

public class MouseInputGetter : MonoBehaviour, IInputGetter
{
    public float Horizontal { get; private set; }

    public float Vertical { get; private set; }

    public bool IsAccelerating { get; private set; }


    private void Update() => GetInput();

    private void GetInput()
    {
        Horizontal = Input.GetAxis("Mouse X");
    }

}