using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public Animator photoAnimator;
    public Animator titleAnimator;

    [SerializeField] private float fadeOutDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartPhotoAppear();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        fadeScreen.StartFade(Color.clear, Color.black, fadeOutDuration);
        yield return new WaitForSeconds(fadeOutDuration);
        SceneLoader.Instance.GoToMainTitle();
    }
}
