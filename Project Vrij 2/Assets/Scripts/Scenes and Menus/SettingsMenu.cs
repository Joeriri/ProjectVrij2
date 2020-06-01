using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;

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
        // zet fmod volume naar nieuwe volume
        // Freek: zet hier de command dat het volume van de fmod sfx mixer naar newSFXVolume gaat
    }

    public void SetMusicVolume()
    {
        // nieuw volume komt binnen als een waarde van 0 tot 10 maar wordt een float van 0.0 tot 1.0
        float newMusicVolume = musicVolumeSlider.value / musicVolumeSlider.maxValue;
        // sla muziek volume op in data
        Data.instance.musicVolume = newMusicVolume;
        // zet fmod volume naar nieuwe volume
        // Freek: zet hier de command dat het volume van de fmod muziek mixer naar newMusicVolume gaat
    }

    public void OpenSettingsMenu()
    {
        gameObject.SetActive(true);
        // laad opgeslagen audio volume data in de sliders
        musicVolumeSlider.value = Data.instance.musicVolume * musicVolumeSlider.maxValue;
        sfxVolumeSlider.value = Data.instance.sfxVolume * sfxVolumeSlider.maxValue;
    }

    public void CloseSettingsMenu()
    {
        gameObject.SetActive(false);
    }
}
