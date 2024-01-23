using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public float lowVolumeLevel = 0.6f;
    public float normalVolumeLevel = 0.8f;
    public float volumeChangeStartDelay = 110.0f; // Delay before volume change
    public float volumeChangeDuration = 75.0f; // Duration for low volume

    private bool isVolumeLowered = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Change volume after a specified delay
        Invoke("LowerVolume", volumeChangeStartDelay);
    }

    void LowerVolume()
    {
        audioSource.volume = lowVolumeLevel;
        isVolumeLowered = true;
        // Reset volume after specified duration
        Invoke("ResetVolume", volumeChangeDuration);
    }

    void ResetVolume()
    {
        if (isVolumeLowered)
        {
            audioSource.volume = normalVolumeLevel;
            isVolumeLowered = false;
        }
    }
}
