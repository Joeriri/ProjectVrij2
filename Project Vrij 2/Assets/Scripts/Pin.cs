using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(LineRenderer))]
public class Pin : MonoBehaviour
{
    public Pin connectedPin;
    public bool isBossPin;
    public bool dragged;
    public bool interactable;
    LineRenderer lr;
    PinManager pinManager;
    public SpriteRenderer thread;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        pinManager = FindObjectOfType<PinManager>();
        thread = transform.Find("Thread").GetComponentInChildren<SpriteRenderer>();
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
            //lr.SetPosition(0, transform.position);
            //lr.SetPosition(1, connectedPin.transform.position);
            float angle = Mathf.Atan2(connectedPin.transform.position.y - transform.position.y, connectedPin.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            thread.transform.rotation = Quaternion.Euler(0, 0, angle);
            thread.size = new Vector2(Vector2.Distance(transform.position, connectedPin.transform.position), 1f);
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
                        ////lr.enabled = true;
                        thread.enabled = true;
                        // configure new pin
                        newPin.connectedPin = this;

                        // play sound
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Pin");
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

                        // play sound
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Pin");
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
