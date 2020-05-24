using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBounds : MonoBehaviour
{
    public static BoardBounds Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 ClampInBounds(Vector3 targetPosition, Vector3 extends)
    {
        // get board bounds
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        // get extends of object size
        float horizExtent = extends.x;
        float vertExtent = extends.y;
        // get mins and maxs of boundry box
        float minX = bounds.min.x + horizExtent;
        float maxX = bounds.max.x - horizExtent;
        float minY = bounds.min.y + vertExtent;
        float maxY = bounds.max.y - vertExtent;
        // clamp that shit
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(targetPosition.x, minX, maxX), Mathf.Clamp(targetPosition.y, minY, maxY), targetPosition.z);
        // return that shit
        return clampedPosition;
    }
}
