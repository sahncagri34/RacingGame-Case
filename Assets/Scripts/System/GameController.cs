using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;


    [SerializeField] UIController uiController;

    public event Action<bool> OnControlToggled;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }
    private async void Start()
    {
        await CountDown();

        ToggleControls(true);
    }

    private async Task CountDown()
    {
        for (int i = 5; i >= 0; i--)
        {
            uiController.ShowNotification(i.ToString());
            await Task.Delay(1000);
        }

        uiController.ShowNotification("GO!");
        await Task.Delay(300);
        uiController.ShowNotification("");
    }

    public void ToggleControls(bool isActive)
    {
        OnControlToggled(isActive);
    }
    public void FinishTheGame()
    {
        uiController.ShowNotification("FINISH!");
        ToggleControls(false);
    }

    public void ShowElapsedTime(float elapsedTime)
    {
        uiController.ShowElapsedTime(elapsedTime);
    }

    public void SetMaxRPMOnSpeedMeter(float maxRPM)
    {
        uiController.SetMaxRPMOnSpeedMeter(maxRPM);
    }

    public void SetNeedleAngle(float currentRPM, int currentGearIndex, float kmh)
    {
        uiController.SetNeedleAngle(currentRPM, currentGearIndex, kmh);
    }
}
