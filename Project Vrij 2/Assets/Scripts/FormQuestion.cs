using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormQuestion : MonoBehaviour
{
    [Header("References")]
    public Text questionText;

    [Header("Don't touch these")]
    public string question;
    public Clue[] clueAnswers;

    public List<Clue> selectedEvidence;

    // Start is called before the first frame update
    void Start()
    {
        questionText.text = question;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQuestionButtonClicked()
    {
        FormManager.Instance.EnterSelectMode();
    }

    void CheckAnswer()
    {

    }
}
