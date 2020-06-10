using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// source for camera dragging: https://forum.unity.com/threads/click-drag-camera-movement.39513/#post-1939243
// source for camera bounds, kinda: https://forum.unity.com/threads/click-drag-camera-movement.39513/#post-2968916
// source for zooming: https://www.youtube.com/watch?v=jmTUUP33GHs

public class CameraDragMove : MonoBehaviour
{
    public bool canNavigate = true;

    [Header("Canvas")]
    public GraphicRaycaster graphicRaycaster;
    public EventSystem eventSystem;
    PointerEventData pointerEventData;

    [Header("Dragging")]
    public BoxCollider2D CameraClampBox;
    private Vector3 originalCameraPos;
    private Vector3 origin;
    private Vector3 difference;
    private bool drag = false;

    [Header("Zooming")]
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float minScreenSize = 1f;
    [SerializeField] private float maxScreenSize = 10f;
    [SerializeField] private float smoothing = 1f;
    private float targetZoom;
    private float zoomVelocity;

    void Start()
    {
        originalCameraPos = Camera.main.transform.position;
        targetZoom = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        if (canNavigate)
        {
            // DRAGGING

            // mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // shoot a ray to check if we are not clicking anything else
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector3.zero);

                if (!hit && !MouseIsOverUI())
                {
                    if (drag == false)
                    {
                        drag = true;
                        origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        MouseCursor.Instance.SetCursor("hold");
                    }
                }
            }

            // mouse button is down
            if (Input.GetMouseButton(0))
            {
                difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            }

            // mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                if (drag)
                {
                    drag = false;

                    MouseCursor.Instance.SetCursor("point");
                }
            }

            if (drag == true)
            {
                Vector3 targetPosition = origin - difference;
                Camera.main.transform.position = ClampCameraPosition(targetPosition);
            }

            // ZOOMING

            float scrollData;
            scrollData = Input.GetAxis("Mouse ScrollWheel");

            targetZoom -= scrollData * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minScreenSize, maxScreenSize);

            Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoom, ref zoomVelocity, smoothing);
            Camera.main.transform.position = ClampCameraPosition(Camera.main.transform.position);

        }
    }

    public void ResetCameraPosition()
    {
        Camera.main.transform.position = originalCameraPos;
    }

    Vector3 ClampCameraPosition(Vector3 targetPosition)
    {
        // get extends of camera size
        float vertExtent = Camera.main.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        return BoardBounds.Instance.ClampInBounds(targetPosition, new Vector3(horizExtent, vertExtent, 0f));
    }

    public bool MouseIsOverUI()
    {
        bool mouseIsOverUI = false;

        // Set up the new Pointer Event
        pointerEventData = new PointerEventData(eventSystem);
        //Set the Pointer Event Position to that of the mouse position
        pointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            mouseIsOverUI = true;
        }

        return mouseIsOverUI;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(CameraClampBox.transform.position, CameraClampBox.bounds.size);
    }
}
