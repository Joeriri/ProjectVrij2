using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// source for camera dragging: https://forum.unity.com/threads/click-drag-camera-movement.39513/#post-1939243
// source for camera bounds, kinda: https://forum.unity.com/threads/click-drag-camera-movement.39513/#post-2968916
// source for zooming: https://www.youtube.com/watch?v=jmTUUP33GHs

public class CameraDragMove : MonoBehaviour
{
    private Vector3 originalCameraPos;
    private Vector3 origin;
    private Vector3 difference;
    private bool Drag = false;

    [Header("Zooming")]
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float minScreenSize = 1f;
    [SerializeField] private float maxScreenSize = 10f;
    [SerializeField] private float smoothing = 1f;
    private float targetZoom;
    private float zoomVelocity;

    public BoxCollider2D CameraClampBox;

    void Start()
    {
        originalCameraPos = Camera.main.transform.position;
        targetZoom = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        // DRAGGING

        if (Input.GetMouseButton(0))
        {
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }

        if (Drag == true)
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

    public void ResetCameraPosition()
    {
        Camera.main.transform.position = originalCameraPos;
    }

    Vector3 ClampCameraPosition(Vector3 targetPosition)
    {
        float vertExtent = Camera.main.orthographicSize;
        float horizExtent = vertExtent * Screen.width / Screen.height;

        float minX = CameraClampBox.bounds.min.x + horizExtent;
        float maxX = CameraClampBox.bounds.max.x - horizExtent;
        float minY = CameraClampBox.bounds.min.y + vertExtent;
        float maxY = CameraClampBox.bounds.max.y - vertExtent;

        Vector3 clampedPosition = new Vector3(Mathf.Clamp(targetPosition.x, minX, maxX), Mathf.Clamp(targetPosition.y, minY, maxY), targetPosition.z);

        return clampedPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(CameraClampBox.transform.position, CameraClampBox.bounds.size);
    }
}
