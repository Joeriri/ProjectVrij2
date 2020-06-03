using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormQuestion : MonoBehaviour
{
    [Header("References")]
    public Text questionText;
    public Image solveStateImage;

    [Header("Other")]
    public CaseQuestion questionInfo;
    //Clue[] oldSelectedClues;

    // Start is called before the first frame update
    void Start()
    {
        questionText.text = questionInfo.question;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnQuestionButtonClicked()
    {
        FormManager.Instance.EnterSelectMode();
        FormManager.Instance.currentQuestion = this;
        //oldSelectedClues = questionInfo.selectedEvidence.ToArray();
    }

    public void OnNewEvidenceSelected()
    {
        questionInfo.solveState = CaseQuestion.SolveStates.Pending;
        solveStateImage.color = Color.yellow;
    }

    public void CheckAnswer()
    {
        //// check if the selected evidence list is longer or shorter than the correct answers array.
        //if (questionInfo.selectedEvidence.Count != questionInfo.clueAnswers.Length)
        //{
        //    // the player selected too much or too little evidence
        //    FailQuestion();
        //}
        //else
        //{
        //    // the player selected the rigth amount of evidence, but is it correct?
        //    int CorrectEvidenceCounter = 0;

        //    // for each clue in the selected evidence list, check if the same clue is in the correct answers array.
        //    foreach (Clue possibleEvidence in questionInfo.selectedEvidence)
        //    {
        //        foreach(Clue correctAnswer in questionInfo.clueAnswers)
        //        {
        //            if (possibleEvidence == correctAnswer) CorrectEvidenceCounter++;
        //        }
        //    }

        //    if (CorrectEvidenceCounter == questionInfo.clueAnswers.Length)
        //    {
        //        // the player selected the right clues!
        //        SolveQuestion();
        //    }
        //    else
        //    {
        //        // the player selected the wrong clues
        //        FailQuestion();
        //    }
        //}

        bool hasCorrectEvidence = true;
        // check if selected clues are all correct answers
        foreach (Clue clue in questionInfo.selectedEvidence)
        {
            bool clueIsEvidence = false;
            foreach (Clue correctAnswer in questionInfo.clueAnswers)
            {
                if (clue == correctAnswer) clueIsEvidence = true;
            }
            if (!clueIsEvidence) hasCorrectEvidence = false;
        }
        // check if selected clues contain all the correct answers
        foreach (Clue correctAnswer in questionInfo.clueAnswers)
        {
            if (!questionInfo.selectedEvidence.Contains(correctAnswer))
            {
                hasCorrectEvidence = false;
            }
        }
        // do the right thing
        if (hasCorrectEvidence)
        {
            SolveQuestion();
        }
        else
        {
            FailQuestion();
        }
    }

    void FailQuestion()
    {
        questionInfo.solveState = CaseQuestion.SolveStates.Wrong;
        solveStateImage.color = Color.red;
    }

    void SolveQuestion()
    {
        questionInfo.solveState = CaseQuestion.SolveStates.Solved;
        solveStateImage.color = Color.green;
    }
}
