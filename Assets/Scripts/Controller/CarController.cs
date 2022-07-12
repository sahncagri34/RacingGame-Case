using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleratingFactor;
    [SerializeField] private int rpmFactor;

    private float _maxRpm;
    private bool isControlActive;

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }
    public float MaxRPM{
        get
        {
            if(_maxRpm == 0) 
            _maxRpm = maxSpeed / gearController.GetLastGearCircumference();

            return _maxRpm;
        }
    }
    public float RpmFactor{
        get{
            return rpmFactor;
        }
    }

    public GearController gearController;
    private IInputGetter movementInputGetter;

    void Awake() => movementInputGetter = GetComponent<IInputGetter>();

    private void Start()
    {
        GameController.Instance.OnControlToggled += OnControlToggled;
        gearController = new GearController(this);
        UIController.Instance.SetMaxRPMOnSpeedMeter(MaxRPM);
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
        : -(acceleratingFactor * 3))
        * Time.deltaTime);

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        Vector3 movement = Vector3.forward * currentSpeed * Time.deltaTime;

        gearController.SetGear(currentSpeed,movementInputGetter.IsAccelerating);

        transform.Translate(movement);
    }

    private void OnControlToggled(bool isActive)
    {
        isControlActive = isActive;
    }
}
