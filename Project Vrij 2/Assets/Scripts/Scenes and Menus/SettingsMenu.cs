using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class SettingsMenu : MonoBehaviour
{
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

    FMOD.Studio.Bus MusicBus;
    FMOD.Studio.Bus sfxBus;

    static public bool settingsMenuIsOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // when settings menu is open and ESC is pressed, close menu
        if (Input.GetKeyDown(KeyCode.Escape) && settingsMenuIsOpen)
        {
            CloseSettingsMenu();
        }
    }

    public void SetSFXVolume()
    {
        // nieuw volume komt binnen als een waarde van 0 tot 10 maar wordt een float van 0.0 tot 1.0
        float newSFXVolume = sfxVolumeSlider.value / sfxVolumeSlider.maxValue;
        // sla sfx volume op in data
        Data.instance.sfxVolume = newSFXVolume;
        // setting slider value to fmod bus
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/sfxBus");
        sfxBus.setVolume(newSFXVolume);
    }

    public void SetMusicVolume()
    {
        // nieuw volume komt binnen als een waarde van 0 tot 10 maar wordt een float van 0.0 tot 1.0
        float newMusicVolume = musicVolumeSlider.value / musicVolumeSlider.maxValue;
        // sla muziek volume op in data
        Data.instance.musicVolume = newMusicVolume;
        // setting slider value to fmod bus
        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/MusicBus");
        MusicBus.setVolume(newMusicVolume);
    }

    public void OpenSettingsMenu()
    {
        // open menu
        gameObject.SetActive(true);
        settingsMenuIsOpen = true;
        // laad opgeslagen audio volume data in de sliders
        musicVolumeSlider.value = Data.instance.musicVolume * musicVolumeSlider.maxValue;
        sfxVolumeSlider.value = Data.instance.sfxVolume * sfxVolumeSlider.maxValue;
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void CloseSettingsMenu()
    {
        // close menu
        gameObject.SetActive(false);
        settingsMenuIsOpen = false;
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }
}
