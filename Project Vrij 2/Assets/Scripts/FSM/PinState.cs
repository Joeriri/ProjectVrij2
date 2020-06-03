using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PinState : GameState
{
    public Button pinButton;

    // called every frame when in this state.
    public override void OnStateUpdate()
    {

    }

    // called when entering this state.
    public override void OnStateEnter()
    {
        Debug.Log("Entering Pin State");

        pinButton.onClick.AddListener(OnPinButtonPressed);
    }

    // called when exiting this state.
    public override void OnStateExit()
    {
        Debug.Log("Exiting Pin State");

        pinButton.onClick.RemoveAllListeners();
    }

    void OnPinButtonPressed()
    {
        Debug.Log("pin button pressed");
        GameStateMachine.Instance.SetGameState(GameStateMachine.Instance.cluesState);
    }
}
