using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

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
            UIController.Instance.ShowNotification(i.ToString());
            await Task.Delay(1000);
        }

        UIController.Instance.ShowNotification("GO!");
        await Task.Delay(300);
        UIController.Instance.ShowNotification("");
    }

    public void ToggleControls(bool isActive)
    {
        OnControlToggled(isActive);
    }
}
