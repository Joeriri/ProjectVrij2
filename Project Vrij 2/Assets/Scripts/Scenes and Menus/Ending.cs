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
        // do fade
        fadeScreen.StartFade(fadeIn.startColor, fadeIn.endColor, fadeIn.duration, fadeIn.fadeCurve);
        yield return new WaitForSeconds(fadeIn.duration);
        // drop the photo
        StartPhotoAppear();
    }

    public void StartPhotoAppear()
    {
        // do photo drop anim
        photoAnimator.Play("Photo Appear");
    }

    public void StartPhotoDisappear()
    {
        // do photo move up out of screen anim
        photoAnimator.Play("Photo Disappear");
    }

    public void StartTitleAppear()
    {
        // do title move up intro screen anim 
        titleAnimator.Play("Title Appear");
    }

    public void StartFadeOut()
    {
        // start fade
        StartCoroutine(FadeToMenu());
    }

    IEnumerator FadeToMenu()
    {
        // do fade
        fadeScreen.StartFade(fadeOut.startColor, fadeOut.endColor, fadeOut.duration, fadeOut.fadeCurve);
        yield return new WaitForSeconds(fadeOut.duration);
        // go to main title scene
        SceneLoader.Instance.GoToMainTitle();
    }
}
