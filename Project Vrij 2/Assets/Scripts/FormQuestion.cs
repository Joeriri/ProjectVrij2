using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormQuestion : MonoBehaviour
{
    [Header("References")]
    public Text questionText;
    public Image solveStateImage;

    [Header("Tick Box")]
    public Sprite openSprite;
    public Sprite pendingSprite;
    public Sprite falseSprite;
    public Sprite solvedSprite;

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
        solveStateImage.sprite = pendingSprite;
    }

    public void CheckAnswer()
    {
        bool hasCorrectEvidence = true;

        // check if selected clues contain all the required answers
        foreach (Clue requiredAnswer in questionInfo.requiredClueAnswers)
        {
            if (!questionInfo.selectedEvidence.Contains(requiredAnswer))
            {
                hasCorrectEvidence = false;
            }
        }

        // check if all selected clues are either required or optional answers
        foreach (Clue selectedClue in questionInfo.selectedEvidence)
        {
            bool clueIsEvidence = false;

            // check the required answers and optional answers
            foreach (Clue requiredAnswer in questionInfo.requiredClueAnswers)
            {
                if (selectedClue == requiredAnswer) clueIsEvidence = true;
            }
            foreach (Clue optionalAnswer in questionInfo.optionalClueAnswers)
            {
                if (selectedClue == optionalAnswer) clueIsEvidence = true;
            }

            // if a clue isn't an answer, the player did not give the correct evidence. End this loop also.
            if (!clueIsEvidence)
            {
                hasCorrectEvidence = false;
                break;
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
        solveStateImage.sprite = falseSprite;
    }

    void SolveQuestion()
    {
        questionInfo.solveState = CaseQuestion.SolveStates.Solved;
        solveStateImage.sprite = solvedSprite;
    }
}
