using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void GoBackToMenu()
    {
        ResumeGame();
        SceneLoader.Instance.GoToMainTitle();
    }
}
