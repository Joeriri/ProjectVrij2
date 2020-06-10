using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoard : MonoBehaviour
{
    public FadeScreen fadescreen;
    public PauseMenu pauseMenu;

    static public PinBoard Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Pause / unpause game by pressing Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (!PauseMenu.gameIsPaused)
            //    pauseMenu.PauseGame();
            //else
            //    pauseMenu.ResumeGame();

            SceneLoader.Instance.GoToMainTitle();
        }
    }

    public void WinGame()
    {
        StartCoroutine(FadeToEnding());
    }

    IEnumerator FadeToEnding()
    {
        float fadeOutDuration = 5f;

        fadescreen.gameObject.SetActive(true);
        fadescreen.StartFade(Color.clear, Color.black, fadeOutDuration);
        yield return new WaitForSeconds(fadeOutDuration);
        SceneLoader.Instance.GoToEnding();
    }
}
