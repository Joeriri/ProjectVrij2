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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        fadeScreen.gameObject.SetActive(true);

        fadeScreen.StartFade(Color.black, Color.clear, enterFadeDuration, fadeInCurve);
        yield return new WaitForSeconds(enterFadeDuration);

        fadeScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenLetter()
    {
        letterScreen.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void OnCloseLetterButtonClicked()
    {
        // close letter
        letterScreen.SetActive(false);
        // start pan and zoom animation
        Camera.main.GetComponent<Animator>().Play("Camera Zoom");

        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void StartExitFade()
    {
        StartCoroutine(ExitFade());
    }

    IEnumerator ExitFade()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.StartFade(Color.clear, Color.black, exitFadeDuration, fadeOutCurve);
        yield return new WaitForSeconds(exitFadeDuration);
        SceneLoader.Instance.GoToPinBoard();
    }
}
