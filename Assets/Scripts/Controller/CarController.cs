using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleratingFactor;
    [SerializeField] private int rpmFactor;
    [SerializeField] private float rotateWheelSpeed;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private GearController gearController;
    [SerializeField] private AudioController audioController;

    private float currentSpeed;
    private float _maxRpm;

    private bool isControlActive;

    private IInputGetter movementInputGetter;
    private Timer timer;

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }
    public float MaxRPM
    {
        get
        {
            if (_maxRpm == 0)
                _maxRpm = maxSpeed / gearController.GetLastGearCircumference();

            return _maxRpm;
        }
    }
    public float RpmFactor
    {
        get
        {
            return rpmFactor;
        }
    }



    void Awake() => movementInputGetter = GetComponent<IInputGetter>();

    private void Start() => Initialize();
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

        gearController.SetGear(currentSpeed, movementInputGetter.IsAccelerating);
        timer.Run();

        RotateWheels();
        transform.Translate(movement);
    }
    private void Initialize()
    {
        GameController.Instance.OnControlToggled += OnControlToggled;

        gearController = new GearController(this,audioController);
        timer = new Timer();

        UIController.Instance.SetMaxRPMOnSpeedMeter(MaxRPM);
        audioController.SetMaxRPM(MaxRPM);
    }


    private void OnControlToggled(bool isActive)
    {
        isControlActive = isActive;
    }
    private void RotateWheels()
    {
        var angle = (currentSpeed % 360) * rotateWheelSpeed;
        foreach (var item in wheels)
        {
            item.Rotate(new Vector3(angle, 0, 0), Space.Self);
        }
    }
}
