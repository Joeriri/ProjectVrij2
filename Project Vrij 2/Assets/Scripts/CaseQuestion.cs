using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaseQuestion
{
    public string question;
    //public Clue[] clueAnswers;
    public Clue[] requiredClueAnswers;
    public Clue[] optionalClueAnswers;
    public List<Clue> selectedEvidence;
    public enum SolveStates
    {
        Clear,
        Pending,
        Wrong,
        Solved
    }
    public SolveStates solveState;
}
