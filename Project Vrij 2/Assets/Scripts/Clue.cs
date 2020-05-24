using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    // state
    public enum ClueStates
    {
        Organize,
        Pin,
        Frozen,
        Select
    }
    public ClueStates state;

    // Organization
    private float minDistanceBeforeDrag = 5f;

    private Vector3 offset;
    private Vector2 originalMousePos;
    private bool dragging = false;

    // Pinning
    private GameObject pinPrefab;

    private BoxCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();

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

    #region Mouse Actions

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    private void OnMouseDown()
    {
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeMouseDown();
                break;
            case ClueStates.Pin:
                OnPinMouseDown();
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
            case ClueStates.Pin:
                OnPinMouseUp();
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
            case ClueStates.Pin:
                OnPinClicked();
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
            case ClueStates.Pin:
                break;
            case ClueStates.Frozen:
                break;
        }
    }

    #endregion

    #region Organize State Functions

    void OnOrganizeMouseDown()
    {
        originalMousePos = Input.mousePosition;
        PickUpClue();
        MouseCursor.Instance.SetCursor("click");
    }

    void OnOrganizeMouseUp()
    {
        dragging = false;
        MouseCursor.Instance.SetCursor("point");
    }

    void OnOrganizeClicked()
    {
        if (!dragging)
        {
            // when the mouse button is released and we weren't dragging, we have been clicked!
            Debug.Log("clicked");
        }

        dragging = false;
    }

    void OnOrganizeDrag()
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

    void PickUpClue()
    {
        ClueManager.Instance.clueSortation.Remove(this);
        ClueManager.Instance.clueSortation.Add(this);
        ClueManager.Instance.SortCluesAlongZ();
    }

    #endregion

    #region Pin State Functions

    void OnPinMouseDown()
    {

    }

    void OnPinMouseUp()
    {

    }

    void OnPinClicked()
    {

    }

    #endregion
}
