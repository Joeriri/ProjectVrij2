using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    public GameObject formScreen;
    public GameObject selectionHeader;
    public GameObject questionsContainer;

    [Header("Case Questions")]
    public GameObject formQuestionPrefab;
    [SerializeField] private float questionBoxSpacing = 1f;
    public CaseQuestion[] caseQuestions;

    private Button pinButton;
    private Button formButton;
    private Animator formScreenAnimator;

    public FormQuestion currentQuestion;

    List<FormQuestion> formQuestions = new List<FormQuestion>();

    static public FormManager Instance;

    private void Awake()
    {
        Instance = this;

        pinButton = GameObject.FindGameObjectWithTag("Pin Button").GetComponent<Button>();
        formButton = GameObject.FindGameObjectWithTag("Form Button").GetComponent<Button>();
        formScreenAnimator = formScreen.GetComponent<Animator>();

        foreach (CaseQuestion cq in caseQuestions)
        {
            // make a question on the form
            GameObject newFQObject = Instantiate(formQuestionPrefab, questionsContainer.transform);
            FormQuestion newFQ = newFQObject.GetComponent<FormQuestion>();

            // add the form question to the array
            formQuestions.Add(newFQ);

            // position form question in the form
            RectTransform fQTrans = newFQ.GetComponent<RectTransform>();
            RectTransform containerTrans = questionsContainer.GetComponent<RectTransform>();
            float questionBoxHeight = fQTrans.sizeDelta.y;
            fQTrans.localPosition = new Vector3(0, (containerTrans.sizeDelta.y * 0.5f) - (fQTrans.sizeDelta.y * 0.5f) - (fQTrans.sizeDelta.y + questionBoxSpacing) * System.Array.IndexOf(caseQuestions, cq), 0);

            // assign variables
            newFQ.questionInfo.question = cq.question;
            //newFQ.questionInfo.clueAnswers = cq.clueAnswers;
            newFQ.questionInfo.requiredClueAnswers = cq.requiredClueAnswers;
            newFQ.questionInfo.optionalClueAnswers = cq.optionalClueAnswers;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FormButtonPressed()
    {
        OpenForm();
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Form");
    }

    public void OpenForm()
    {
        // show form screen
        formScreen.SetActive(true);
        formScreenAnimator.Play("Case Form Open");
        // de-activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetPinsInteractable(false);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = false;
    }

    public void CloseForm()
    {
        // hide form screen
        formScreen.SetActive(false);
        // activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Organize);
        PinManager.Instance.SetPinsInteractable(true);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = true;
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Form Put Down");
    }

    // SELECTION MODE

    public void EnterSelectMode()
    {
        // hide form screen
        formScreen.SetActive(false);
        // show selection header
        selectionHeader.SetActive(true);
        // activate clues, pins, threads and camera navigation. Set clue state to Select
        ClueManager.Instance.SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetThreadsInteractable(true);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = true;
        // disable buttons
        pinButton.interactable = false;
        formButton.interactable = false;
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click 2");
    }

    public void ExitSelectMode()
    {
        // show form screen
        formScreen.SetActive(true);
        // hide selection header
        selectionHeader.SetActive(false);
        // de-activate clues, pins, threads and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetThreadsInteractable(false);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = false;
        // enable buttons
        pinButton.interactable = true;
        formButton.interactable = true;
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click 2");

        // tell the current form question that there is new evidence selected
        currentQuestion.OnNewEvidenceSelected();
    }

    public void CheckPendingQuestions()
    {
        int correctSolveCounter = 0;

        // let every pending form question check their answer
        foreach (FormQuestion fq in formQuestions)
        {
            if (fq.questionInfo.solveState == CaseQuestion.SolveStates.Pending)
            {
                fq.CheckAnswer();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Click 2");
            }

            // check if all questions have been answered correctly
            if (fq.questionInfo.solveState == CaseQuestion.SolveStates.Solved)
            {
                correctSolveCounter++;
            }
            if (correctSolveCounter >= formQuestions.Count)
            {
                // all questions have been solved. Win the game!
                PinBoard.Instance.WinGame();
                Debug.Log("The player has won the game!");
            }

        }
    }
}
