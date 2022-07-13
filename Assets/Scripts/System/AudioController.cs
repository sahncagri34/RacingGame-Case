using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private float maxRPM;
    private float currentPitch;
    private float initialPitch = 1;
    private float maxPitch = 4;

    private void Start() => GameController.Instance.OnControlToggled += OnControlToggled;

    private void OnDestroy() => GameController.Instance.OnControlToggled -= OnControlToggled;

    /// called each frame
    public void SetAudioPitch(float rpm)
    {
         currentPitch = 5 * (rpm / maxRPM);
        currentPitch = Mathf.Clamp(currentPitch, initialPitch, maxPitch);
        audioSource.pitch = currentPitch;
    }

    /// called once at initialization
    public void SetMaxRPM(float maxRPM)
    {
        this.maxRPM = maxRPM;
    }

    private void OnControlToggled(bool isActive)
    {
        audioSource.enabled = isActive;
    }
} 
