using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [Header("Transitions")]
    public FadeScreen fadeScreen;
    public Fade fadeIn;
    public Fade fadeOut;

    [Header("Animations")]
    public Animator photoAnimator;
    public Animator titleAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInSequence());
    }

    IEnumerator FadeInSequence()
    {
        fadeScreen.StartFade(fadeIn.startColor, fadeIn.endColor, fadeIn.duration, fadeIn.fadeCurve);
        yield return new WaitForSeconds(fadeIn.duration);
        StartPhotoAppear();
    }

    public void StartPhotoAppear()
    {
        photoAnimator.Play("Photo Appear");
    }

    public void StartPhotoDisappear()
    {
        photoAnimator.Play("Photo Disappear");
    }

    public void StartTitleAppear()
    {
        titleAnimator.Play("Title Appear");
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeToMenu());
    }

    IEnumerator FadeToMenu()
    {
        fadeScreen.StartFade(fadeOut.startColor, fadeOut.endColor, fadeOut.duration, fadeOut.fadeCurve);
        yield return new WaitForSeconds(fadeOut.duration);
        SceneLoader.Instance.GoToMainTitle();
    }
}
