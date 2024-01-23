using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiManager : MonoBehaviour
{
    public Button settingsButton;
    public Image menuScreen;
    public Image settingScreen;
    public GameObject gameManager;

    public Button muteEnableButton;
    public Button muteDisableButton;
    public Button unmuteEnableButton;
    public Button unmuteDisableButton;

    private List<AudioSource> playingAudioSources = new List<AudioSource>();

    private void Start()
    {

    }

    public void OnMenuButtonPress()
    {
        Time.timeScale = 0;
        menuScreen.gameObject.SetActive(true);
        gameManager.SetActive(false);

    }

    public void OnPlayButtonPress()
    {
        Time.timeScale = 1;
        menuScreen.gameObject.SetActive(false);
        gameManager.SetActive(true);
    }

    public void OnSettingsButtonPress()
    {
        menuScreen.gameObject.SetActive(false);
        settingScreen.gameObject.SetActive(true);

    }

    public void OnReturnButtonPress()
    {
        settingScreen.gameObject.SetActive(false);
        menuScreen.gameObject.SetActive(true);
    }

    public void OnMuteSoundButtonPress()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        playingAudioSources.Clear(); // Clear the list to store currently playing AudioSources

        foreach (AudioSource audioSrc in allAudioSources)
        {
            if (audioSrc.isPlaying)
            {
                playingAudioSources.Add(audioSrc); // Add the AudioSource to the list if it's playing
                audioSrc.Pause(); // Pause the AudioSource
            }
        }
        muteEnableButton.gameObject.SetActive(true);
        muteDisableButton.gameObject.SetActive(false);
        unmuteDisableButton.gameObject.SetActive(true);
        unmuteEnableButton.gameObject.SetActive(false);

    }

    public void OnUnmuteSoundButtonPress()
    {
        foreach (AudioSource audioSrc in playingAudioSources)
        {
            audioSrc.UnPause(); // Resume the AudioSource
        }


        unmuteEnableButton.gameObject.SetActive(true); 
        unmuteDisableButton.gameObject.SetActive(false);
        muteDisableButton.gameObject.SetActive(true);

    }

    public void OnExitButtonPress()
    {
        Application.Quit();
    }
}
