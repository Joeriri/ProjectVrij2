using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseCursor.Instance.SetCursor("point");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseCursor.Instance.SetCursor("point");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MouseCursor.Instance.SetCursor("click");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MouseCursor.Instance.SetCursor("point");
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
