using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoard : MonoBehaviour
{
    public PauseMenu pauseMenu;

    [Header("Transitions")]
    public FadeScreen fadescreen;
    public Fade fadeIn;
    public Fade fadeOut;

    static public PinBoard Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnterFade());
    }

    IEnumerator EnterFade()
    {
        // de-activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetPinsInteractable(false);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = false;
        // fade
        fadescreen.gameObject.SetActive(true);
        fadescreen.StartFade(fadeIn.startColor, fadeIn.endColor, fadeIn.duration, fadeIn.fadeCurve);
        yield return new WaitForSeconds(fadeIn.duration);
        fadescreen.gameObject.SetActive(false);
        // activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Organize);
        PinManager.Instance.SetPinsInteractable(true);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Pause / unpause game by pressing Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseMenu.gameIsPaused)
                pauseMenu.PauseGame();
            else
                pauseMenu.ResumeGame();

            //SceneLoader.Instance.GoToMainTitle();
        }
    }

    public void WinGame()
    {
        StartCoroutine(FadeToEnding());
    }

    IEnumerator FadeToEnding()
    {
        fadescreen.gameObject.SetActive(true);
        fadescreen.StartFade(fadeOut.startColor, fadeOut.endColor, fadeOut.duration, fadeOut.fadeCurve);
        yield return new WaitForSeconds(fadeOut.duration);
        SceneLoader.Instance.GoToEnding();
    }
}
