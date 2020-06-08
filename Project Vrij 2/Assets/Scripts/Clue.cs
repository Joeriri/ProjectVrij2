using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Clue : MonoBehaviour
{
    [Header("States")]
    public ClueTypes type;
    public ClueStates state;

    [Header("Audio")]
    // extra fmod event to play on click
    public string playAfterClick;

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
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeMouseEnter();
                break;
            case ClueStates.Frozen:
                break;
        }
    }

    private void OnMouseExit()
    {
        switch (state)
        {
            case ClueStates.Organize:
                OnOrganizeMouseExit();
                break;
            case ClueStates.Frozen:
                break;
        }
    }

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

    void OnOrganizeMouseEnter()
    {
        SetOutlineColor(Color.black);
    }

    void OnOrganizeMouseExit()
    {
        SetOutlineColor(Color.clear);
    }

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
            ClueManager.Instance.OpenItemViewer(this);
            SetOutlineColor(Color.clear);
        }

        dragging = false;
        MouseCursor.Instance.SetCursor("point");
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

    // outline shader source: https://assetstore.unity.com/packages/vfx/shaders/2d-sprite-outline-109669#reviews
    // source for setting shader property: https://nielson.dev/2016/04/2d-sprite-outlines-in-unity
    void SetOutlineColor(Color newColor)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetColor("_SolidOutline", newColor);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
