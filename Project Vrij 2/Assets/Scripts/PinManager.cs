using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject pinPrefab;
    public LayerMask pinsLayer;
    public List<Pin> pins;

    public static PinManager Instance;

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

    public void PinButtonPressed()
    {
        CreatePin();
    }

    public Pin CreatePin()
    {
        // create a new pin at mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
        GameObject newPinObject = Instantiate(pinPrefab, mousePos, Quaternion.Euler(Vector3.zero));
        newPinObject.layer = LayerMask.NameToLayer("Pins");
        // get Pin script and add to pin list
        Pin newPin = newPinObject.GetComponent<Pin>();
        pins.Add(newPin);
        // disable line renderer and and set variables
        ////newPin.GetComponent<LineRenderer>().enabled = false;
        newPin.thread.enabled = false;
        newPin.dragged = true;
        newPin.interactable = true;
        // return
        return newPin;
    }

    public void DeletePin(Pin trashPin)
    {
        pins.Remove(trashPin);
        Destroy(trashPin.gameObject);
    }

    public void SetPinsInteractable(bool interactable)
    {
        foreach (Pin everyPin in pins)
        {
            everyPin.interactable = interactable;
        }
    }
}
