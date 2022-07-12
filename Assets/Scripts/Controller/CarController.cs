using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxRPM;
    [SerializeField] private float acceleratingFactor;

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }
    public float MaxRPM{
        get{
            return maxRPM;
        }
    }
    private bool isControlActive;

    public GearController gearController;
    private IInputGetter movementInputGetter;

    void Awake() => movementInputGetter = GetComponent<IInputGetter>();

    private void Start()
    {
        GameController.Instance.OnControlToggled += OnControlToggled;
        UIController.Instance.SetMaxRPMOnSpeedMeter(maxRPM);
        gearController = new GearController(this);
    }
    private void OnDestroy()
    {
        GameController.Instance.OnControlToggled -= OnControlToggled;
    }


    void Update()
    {
        if (!isControlActive) return;


        currentSpeed += ((movementInputGetter.IsAccelerating == true
        ? acceleratingFactor
        : -(acceleratingFactor * 50))
        * Time.deltaTime);

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        Vector3 movement = Vector3.forward * currentSpeed * Time.deltaTime;

        gearController.SetGear(currentSpeed);

        transform.Translate(movement);
    }

    private void OnControlToggled(bool isActive)
    {
        isControlActive = isActive;
    }
}
