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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
        gameObject.SetActive(true);
        // laad opgeslagen audio volume data in de sliders
        musicVolumeSlider.value = Data.instance.musicVolume * musicVolumeSlider.maxValue;
        sfxVolumeSlider.value = Data.instance.sfxVolume * sfxVolumeSlider.maxValue;
    }

    public void CloseSettingsMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");  
        gameObject.SetActive(false);
    }
}
