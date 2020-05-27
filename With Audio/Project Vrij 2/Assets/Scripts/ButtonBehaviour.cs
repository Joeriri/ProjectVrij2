using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCursorEnter()
    {
        MouseCursor.Instance.SetCursor("point");
    }

    public void OnCursorExit()
    {
        MouseCursor.Instance.SetCursor("point");
    }

    public void OnCursorDown()
    {
        MouseCursor.Instance.SetCursor("click");
    }

    public void OnCursorUp()
    {
        MouseCursor.Instance.SetCursor("point");
    }
}
