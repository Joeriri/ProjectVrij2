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

    static public FormManager Instance;

    private void Awake()
    {
        Instance = this;

        foreach (CaseQuestion cq in caseQuestions)
        {
            // make a question on the form
            GameObject newFQObject = Instantiate(formQuestionPrefab, questionsContainer.transform);
            FormQuestion newFQ = newFQObject.GetComponent<FormQuestion>();

            // position form question in the form
            RectTransform fQTrans = newFQ.GetComponent<RectTransform>();
            RectTransform containerTrans = questionsContainer.GetComponent<RectTransform>();
            float questionBoxHeight = fQTrans.sizeDelta.y;
            fQTrans.localPosition = new Vector3(0, (containerTrans.sizeDelta.y * 0.5f) - (fQTrans.sizeDelta.y * 0.5f) - (fQTrans.sizeDelta.y + questionBoxSpacing) * System.Array.IndexOf(caseQuestions, cq), 0);

            // assign variables
            newFQ.question = cq.question;
            newFQ.clueAnswers = cq.clueAnswers;
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
    }

    public void OpenForm()
    {
        // show form screen
        formScreen.SetActive(true);
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
    }

    // SELECTION MODE

    public void EnterSelectMode()
    {
        // hide form screen
        formScreen.SetActive(false);
        // show selection header
        selectionHeader.SetActive(true);
        // activate clues, pins and camera navigation. Set clue state to Select
        ClueManager.Instance.SetClueState(Clue.ClueStates.Select);
        PinManager.Instance.SetPinsInteractable(true);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = true;
    }

    public void ExitSelectMode()
    {
        // show form screen
        formScreen.SetActive(true);
        // hide selection header
        selectionHeader.SetActive(false);
        // de-activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetPinsInteractable(false);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = false;
    }
}
