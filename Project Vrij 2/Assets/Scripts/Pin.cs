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
    public SpriteRenderer threadSprite;
    public Thread thread;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        pinManager = FindObjectOfType<PinManager>();
        thread = GetComponentInChildren<Thread>();
        threadSprite = thread.GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (dragged)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
            transform.position = mousePos;
        }

        // als dit de boss pin is
        if (isBossPin)
        {
            // zet de lijn van deze transform naar de transform van de connected pin
            //lr.SetPosition(0, transform.position);
            //lr.SetPosition(1, connectedPin.transform.position);
            float angle = Mathf.Atan2(connectedPin.transform.position.y - transform.position.y, connectedPin.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            threadSprite.transform.rotation = Quaternion.Euler(0, 0, angle);
            threadSprite.size = new Vector2(Vector2.Distance(transform.position, connectedPin.transform.position), 0.5f);
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
                        thread.gameObject.SetActive(true);
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

                    // once placed, save the clue inside the thread
                    if (isBossPin)
                    {
                        // save clue inside thread
                        thread.firstClue = clickedClue;
                    }
                    else
                    {
                        // save clue inside thread of boss pin
                        connectedPin.thread.secondClue = clickedClue;
                    }
                }
            }
            else
            {
                // if the pin is not dragged, pick it up!
                dragged = true;
                transform.parent = null;

                // if a pin is being replaced and the thread between the pins was selected as evidence, we want to remove the clues of that thread from the selected evidence.
                if (isBossPin)
                {
                    if (thread.selectedAsEvidence) thread.DeselectThreadAsEvidence();
                }
                else
                {
                    if (connectedPin.thread.selectedAsEvidence) connectedPin.thread.DeselectThreadAsEvidence();
                }
            }
        }
    }
}
