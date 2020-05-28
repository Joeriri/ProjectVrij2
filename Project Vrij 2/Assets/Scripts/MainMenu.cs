using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartGameButtonPressed()
    {
        SceneLoader.Instance.GoToPinBoard();
        FMODUnity.RuntimeManager.PlayOneShot("event:/In Game Music");
    }

    public void OnSettingsButtonPressed()
    {
        SceneLoader.Instance.GoToSettings();
    }

    public void OnQuitButtonPressed()
    {
        SceneLoader.Instance.QuitGame();
    }
}
