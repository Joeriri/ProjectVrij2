using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MainMenu : MonoBehaviour
{

    FMOD.Studio.EventInstance Music;

    // Start is called before the first frame update
    void Start()
    {
        // when the game launches, do this once and then never again.
        if (!Data.instance.gameHasLaunched)
        {
            Data.instance.gameHasLaunched = true;
            Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            Music.start();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartGameButtonPressed()
    {
        SceneLoader.Instance.GoToIntro();
        Music.setParameterByName("Music Marker", 1);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void OnSettingsButtonPressed()
    {
        SceneLoader.Instance.GoToSettings();
    }

    public void OnQuitButtonPressed()
    {
        SceneLoader.Instance.QuitGame();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }
}
