using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject letterScreen;
    public PauseMenu pauseMenu;
    public CaseBox caseBox;

    [Header("Transitions")]
    public FadeScreen fadeScreen;
    public Fade fadeIn;
    public Fade fadeOut;

    private Animator letterScreenAnimator;

    static public Intro Instance;

    private void Awake()
    {
        Instance = this;

        letterScreenAnimator = letterScreen.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }

    private void Update()
    {

    }

    IEnumerator StartFade()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(fadeIn.startColor, fadeIn.endColor, fadeIn.duration, fadeIn.fadeCurve);
        yield return new WaitForSeconds(fadeIn.duration);
        fadeScreen.gameObject.SetActive(false);
    }

    public void OnCaseBoxClicked()
    {
        // open letter
        letterScreen.SetActive(true);
        letterScreenAnimator.Play("Case Form Open");
        // play sound
        // FREEK: playe sound
    }

    public void OnCloseLetterButtonClicked()
    {
        // close letter
        letterScreen.SetActive(false);
        // start pan and zoom animation
        Camera.main.GetComponent<Animator>().Play("Camera Zoom");
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Form Put Down");
    }

    public void StartExitFade()
    {
        StartCoroutine(ExitFade());
    }

    IEnumerator ExitFade()
    {
        // do fade
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(fadeOut.startColor, fadeOut.endColor, fadeOut.duration, fadeOut.fadeCurve);
        yield return new WaitForSeconds(fadeOut.duration);
        // go to pinboard scee
        SceneLoader.Instance.GoToPinBoard();
    }
}
