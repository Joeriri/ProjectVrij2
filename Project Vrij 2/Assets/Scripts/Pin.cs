using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Pin : MonoBehaviour
{
    public Pin connectedPin;
    public bool isBossPin;
    public bool dragged;
    public bool interactable;
    LineRenderer lr;
    PinManager pinManager;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        pinManager = FindObjectOfType<PinManager>();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        // als dit de boss pin is
        if (isBossPin)
        {
            // zet de lijn van deze transform naar de transform van de connected pin
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, connectedPin.transform.position);
        }

        if (dragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            transform.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        
    }

    private void OnMouseUp()
    {
        // pin must be interactable
        if (interactable)
        {
            //check if this is the pin we are dragging
            if (dragged)
            {
                Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D clueCollider = Physics2D.OverlapPoint(screenPos, ClueManager.Instance.cluesLayer);

                // check if we hit anything on the clue layer
                if (clueCollider != null)
                {
                    // get clue
                    Clue clickedClue = clueCollider.GetComponent<Clue>();

                    // check if this is the first pin (not yet connected to another pin)
                    if (connectedPin == null)
                    {
                        // create new pin
                        Pin newPin = PinManager.Instance.CreatePin();
                        // place this pin
                        dragged = false;
                        isBossPin = true;
                        transform.parent = clickedClue.transform;
                        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                        connectedPin = newPin;
                        lr.enabled = true;
                        // configure new pin
                        newPin.connectedPin = this;
                    }
                    else if (connectedPin != null) // this is the second pin
                    {
                        // place this pin
                        dragged = false;
                        transform.parent = clickedClue.transform;
                        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

                        // check if this is the same clue as the first pin
                        if (transform.parent == connectedPin.transform.parent)
                        {
                            PinManager.Instance.DeletePin(this);
                            PinManager.Instance.DeletePin(connectedPin);
                        }

                    }
                }
            }
            else
            {
                // if the pin is not dragged, pick it up!
                dragged = true;
                transform.parent = null;
            }
        }
    }
}
