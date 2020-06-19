using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBoard : MonoBehaviour
{
    public FadeScreen fadescreen;
    public PauseMenu pauseMenu;

    [SerializeField] private float fadeInDuration = 3f;
    public AnimationCurve fadeInCurve;
    [SerializeField] private float fadeOutDuration = 5f;
    public AnimationCurve fadeOutCurve;

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
        fadescreen.StartFade(Color.black, Color.clear, fadeInDuration, fadeInCurve);
        yield return new WaitForSeconds(fadeInDuration);
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
        fadescreen.StartFade(Color.clear, Color.black, fadeOutDuration, fadeOutCurve);
        yield return new WaitForSeconds(fadeOutDuration);
        SceneLoader.Instance.GoToEnding();
    }
}
