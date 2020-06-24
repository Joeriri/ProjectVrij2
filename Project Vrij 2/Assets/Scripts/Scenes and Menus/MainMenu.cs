using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Transition")]
    public FadeScreen fadeScreen;
    public Fade fadeIn;
    public Fade fadeOut;

    void Start()
    {
        StartCoroutine(FadeInSequence());
    }

    IEnumerator FadeInSequence()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(fadeIn.startColor, fadeIn.endColor, fadeIn.duration, fadeIn.fadeCurve);
        yield return new WaitForSeconds(fadeIn.duration);
        fadeScreen.gameObject.SetActive(false);
    }

    IEnumerator FadeOutSequence()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(fadeOut.startColor, fadeOut.endColor, fadeOut.duration, fadeOut.fadeCurve);
        yield return new WaitForSeconds(fadeOut.duration);
        // go to intro scene
        SceneLoader.Instance.GoToIntro();
    }

    public void OnStartGameButtonPressed()
    {
        // start fade to intro
        StartCoroutine(FadeOutSequence());
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void OnQuitButtonPressed()
    {
        SceneLoader.Instance.QuitGame();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }
}
