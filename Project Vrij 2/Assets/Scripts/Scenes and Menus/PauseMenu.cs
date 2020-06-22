using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject fadeScreen;

    public static bool gameIsPaused;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                // when not paused and not fading, pause
                if (!fadeScreen.activeInHierarchy && !Tutorial.tutorialScreenIsOpen)
                    PauseGame();
            }
            else
            {
                // when paused and settings menu is not open, unpause
                if (!SettingsMenu.settingsMenuIsOpen)
                    ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        if (SceneManager.GetActiveScene().name == "Pinboard")
        {
            // de-activate clues, pins and camera navigation
            PinBoard.Instance.SetBoardInteractable(false);
        }

        if (SceneManager.GetActiveScene().name == "Intro")
        {
            // de-active case box
            Intro.Instance.caseBox.isInteractable = false;
        }
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Pinboard")
        {
            // activate clues, pins and camera navigation
            PinBoard.Instance.SetBoardInteractable(true);
        }

        if (SceneManager.GetActiveScene().name == "Intro")
        {
            // de-active case box
            Intro.Instance.caseBox.isInteractable = true;
        }
    }

    public void GoBackToMenu()
    {
        ResumeGame();
        SceneLoader.Instance.GoToMainTitle();
    }

    public void OpenTutorial()
    {
        ResumeGame();
        PinBoard.Instance.tutorialScreen.OpenTutorialScreen();
    }
}
