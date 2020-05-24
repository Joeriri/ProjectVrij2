﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject pinPrefab;
    public LayerMask pinsLayer;

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0f);
        GameObject newPinObject = Instantiate(pinPrefab, mousePos, Quaternion.Euler(Vector3.zero));
        newPinObject.layer = LayerMask.NameToLayer("Pins");
        Pin newPin = newPinObject.GetComponent<Pin>();
        
        newPin.GetComponent<LineRenderer>().enabled = false;
        newPin.dragged = true;

        return newPin;
    }
}
