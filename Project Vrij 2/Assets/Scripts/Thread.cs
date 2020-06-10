using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thread : MonoBehaviour
{
    public bool interactable = true;
    public bool selectedAsEvidence;
    public Clue firstClue;
    public Clue secondClue;

    FormQuestion selectedForQuestion;

    Collider2D coll;
    SpriteRenderer sprite;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (interactable) MouseCursor.Instance.SetCursor("click");
    }

    private void OnMouseUp()
    {
        if (interactable) MouseCursor.Instance.SetCursor("point");
    }

    private void OnMouseUpAsButton()
    {
        if (interactable)
        {
            // we can select or deselect this thread as evidence
            if (!selectedAsEvidence)
            {
                selectedForQuestion = FormManager.Instance.currentQuestion;
                SelectThreadAsEvidence();
            }
            else
            {
                DeselectThreadAsEvidence();
            }
        }
    }

    public void SetThreadInteractable(bool isInteractable)
    {
        interactable = isInteractable;
        coll.enabled = isInteractable;
    }

    public void SelectThreadAsEvidence()
    {
        selectedAsEvidence = true;
        // add the clues this thread is connected to to the selected evidence of the current question
        selectedForQuestion.questionInfo.selectedEvidence.Add(firstClue);
        selectedForQuestion.questionInfo.selectedEvidence.Add(secondClue);
        // do a selection effect
        sprite.color = Color.green;
        // play sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/Click");
    }

    public void DeselectThreadAsEvidence()
    {
        selectedAsEvidence = false;
        // remove the clues this thread is connected to from the selected evidence of the current question
        selectedForQuestion.questionInfo.selectedEvidence.Remove(firstClue);
        selectedForQuestion.questionInfo.selectedEvidence.Remove(secondClue);
        // do a selection effect
        sprite.color = Color.white;
    }
}
