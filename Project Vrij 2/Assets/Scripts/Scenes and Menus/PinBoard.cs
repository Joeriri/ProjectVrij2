using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoard : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Tutorial tutorialScreen;

    [Header("Transitions")]
    public FadeScreen fadeScreen;
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


    public void WinGame()
    {
        StartCoroutine(FadeToEnding());
    }

    public void SetBoardInteractable(bool isInteractable)
    {
        ClueManager.Instance.SetClueState((isInteractable? Clue.ClueStates.Organize : Clue.ClueStates.Frozen));
        PinManager.Instance.SetPinsInteractable(isInteractable);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = isInteractable;
    }

    IEnumerator EnterFade()
    {
        // de-activate clues, pins and camera navigation
        SetBoardInteractable(false);
        // fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(fadeIn.startColor, fadeIn.endColor, fadeIn.duration, fadeIn.fadeCurve);
        yield return new WaitForSeconds(fadeIn.duration);
        fadeScreen.gameObject.SetActive(false);
        // activate clues, pins and camera navigation
        SetBoardInteractable(true);
        // open tutorial
        tutorialScreen.OpenTutorialScreen();
    }

    IEnumerator FadeToEnding()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(fadeOut.startColor, fadeOut.endColor, fadeOut.duration, fadeOut.fadeCurve);
        yield return new WaitForSeconds(fadeOut.duration);
        // go to ending scene
        SceneLoader.Instance.GoToEnding();
    }

    public void StartProgressedState()
    {
        // start playing progressed music
        MusicManager.instance.Music.setParameterByName("Music Marker", 2f);
    }
}
