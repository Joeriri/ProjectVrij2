﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // This lets us make SceneLoader a singleton so we can adres it easily
    public static SceneLoader Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Only load scenes with this function, because it adds the current scene to the loadedScenes stack.
    public void LoadScene(string name)
    {
        // add current scene to loadedScenes stack
        Data.instance.loadedScenes.Push(SceneManager.GetActiveScene().name);
        // load the next scene
        SceneManager.LoadScene(name);
    }

    public void LoadPreviousScene()
    {
        if (Data.instance.loadedScenes.Count > 0)
        {
            // load the top scene of the loadedScenes stack and delete it from the stack.
            SceneManager.LoadScene(Data.instance.loadedScenes.Pop());
        }
        else
        {
            Debug.LogError("No previous scene loaded.");
        }
    }

    // FUNCTIONS FOR EASE OF USE

    public void GoToMainTitle()
    {
        LoadScene("MainTitle");
    }

    public void GoToPinBoard()
    {
        LoadScene("PinBoard");
    }

    public void GoToFoyer()
    {
        LoadScene("Foyer");
    }

    public void GoToEnding()
    {
        LoadScene("Ending");
    }

    public void GoToSettings()
    {
        // go to settings scene
        Debug.LogWarning("There is no Settings scene to go to!");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    
}
