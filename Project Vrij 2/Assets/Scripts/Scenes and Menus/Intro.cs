using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject letterScreen;
    [SerializeField] private float enterFadeDuration = 5f;
    public AnimationCurve fadeInCurve;
    [SerializeField] private float exitFadeDuration = 3f;
    public AnimationCurve fadeOutCurve;
    public FadeScreen fadeScreen;

    static public Intro Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(Color.black, Color.clear, enterFadeDuration, fadeInCurve);
        yield return new WaitForSeconds(enterFadeDuration);
        fadeScreen.gameObject.SetActive(false);
    }
    
    public void OnCaseBoxClicked()
    {
        // open letter
        letterScreen.SetActive(true);
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void OnCloseLetterButtonClicked()
    {
        // close letter
        letterScreen.SetActive(false);
        // start pan and zoom animation
        Camera.main.GetComponent<Animator>().Play("Camera Zoom");
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void StartExitFade()
    {
        StartCoroutine(ExitFade());
    }

    IEnumerator ExitFade()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(Color.clear, Color.black, exitFadeDuration, fadeOutCurve);
        yield return new WaitForSeconds(exitFadeDuration);
        // go to pinboard scee
        SceneLoader.Instance.GoToPinBoard();
    }
}
