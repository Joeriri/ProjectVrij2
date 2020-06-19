using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Clue : MonoBehaviour
{
    [Header("States")]
    public ClueTypes type;
    public ClueStates state;
    public bool interactable = true;

    [Header("Audio")]
    // extra fmod event to play on click
    public string playAfterClick;

    private bool falseClick;

    private CameraDragMove camMovement;

    // type
    public enum ClueTypes
    {
        Interview,
        Profile,
        Item,
        Location
    }

    // state
    public enum ClueStates
    {
        Organize,
        Frozen
    }

    // Organization
    private float minDistanceBeforeDrag = 5f;

    private Vector3 offset;
    private Vector2 originalMousePos;
    private bool dragging = false;

    private BoxCollider2D coll;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        camMovement = Camera.main.GetComponent<CameraDragMove>();

        ClueManager.Instance.clueSortation.Add(this);
    }

    private void Start()
    {
        // set variables from clue manager
        minDistanceBeforeDrag = ClueManager.Instance.minDistanceBeforeDrag;
    }

    private void Update()
    {

    }

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {

    }

    #region Mouse Actions



    private void OnMouseDown()
    {
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeMouseDown();
                break;
            case ClueStates.Frozen:
                break;
        }
    }

    private void OnMouseUp()
    {
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeMouseUp();
                break;
            case ClueStates.Frozen:
                break;
        }
    }

    private void OnMouseUpAsButton()
    {
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeClicked();
                break;
            case ClueStates.Frozen:
                break;
        }
    }

    private void OnMouseDrag()
    {
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeDrag();
                break;
            case ClueStates.Frozen:
                break;
        }
    }

    #endregion

    #region Organize State Functions
    
    void OnOrganizeMouseDown()
    {   
        // set falseclick to true if the clue was clicked but actually there was a UI element over it that was clicked
        if (camMovement.MouseIsOverUI())
            falseClick = true;

        if (!falseClick)
        {
            originalMousePos = Input.mousePosition;
            PickUpClue();
            MouseCursor.Instance.SetCursor("click");
        }
    }

    void OnOrganizeMouseUp()
    {
        if (!falseClick)
        {
            dragging = false;
            MouseCursor.Instance.SetCursor("point");
        }

        falseClick = false;
    }

    void OnOrganizeClicked()
    {
        if (!falseClick)
        {
            if (!dragging)
            {
                // when the mouse button is released and we weren't dragging, we have been clicked!
                // make sure there is no UI over this clue that the player wanted to click instead
                if (!camMovement.MouseIsOverUI())
                {
                    ClueManager.Instance.OpenItemViewer(this);
                }
            }

            dragging = false;
            MouseCursor.Instance.SetCursor("point");
        }
    }

    void OnOrganizeDrag()
    {
        if (!falseClick)
        {
            // check if the player dragged the cursor far enough, then we allow the clue to be dragged along
            if (!dragging)
            {
                Vector2 newMousePos = Input.mousePosition;
                float diff = (originalMousePos - newMousePos).magnitude;

                if (diff > minDistanceBeforeDrag)
                {
                    dragging = true;
                    offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
                    MouseCursor.Instance.SetCursor("hold");
                }
            }

            if (dragging)
            {
                Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
                transform.position = BoardBounds.Instance.ClampInBounds(newPos + offset, coll.bounds.extents);
            }
        }
    }

    void PickUpClue()
    {
        ClueManager.Instance.clueSortation.Remove(this);
        ClueManager.Instance.clueSortation.Add(this);
        ClueManager.Instance.SortCluesAlongZ();
    }

    #endregion

    
}
