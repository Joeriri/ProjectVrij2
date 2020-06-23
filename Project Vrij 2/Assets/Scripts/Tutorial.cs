using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutorialScreens;
    private int currentStep = 0;

    public static bool tutorialScreenIsOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // navigate bewteen steps
        if (Input.GetKeyDown(KeyCode.RightArrow))
            GoToNextTutorialStep();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GoToPreviousTutorialStep();

        // force exit tutorial
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTutorialScreen();
        }
    }

    public void OpenTutorialScreen()
    {
        //disable clues, pins, camera movement
        PinBoard.Instance.SetBoardInteractable(false);
        // show screen
        gameObject.SetActive(true);
        tutorialScreenIsOpen = true;
        // go to first step
        GoToTutorialStep(0);

    }

    public void CloseTutorialScreen()
    {
        //enable clues, pins, camera movement
        PinBoard.Instance.SetBoardInteractable(true);
        // hide screen
        gameObject.SetActive(false);
        tutorialScreenIsOpen = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void GoToNextTutorialStep()
    {
        // if we are not at the last step
        if (currentStep < tutorialScreens.Length - 1)
        {
            GoToTutorialStep(currentStep + 1);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
        }
    }

    public void GoToPreviousTutorialStep()
    {
        // if we are not at the first step
        if (currentStep > 0)
        {
            GoToTutorialStep(currentStep - 1);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
        }
    }

    void GoToTutorialStep(int step)
    {
        tutorialScreens[currentStep].SetActive(false);
        currentStep = step;
        tutorialScreens[currentStep].SetActive(true);
    }
}
