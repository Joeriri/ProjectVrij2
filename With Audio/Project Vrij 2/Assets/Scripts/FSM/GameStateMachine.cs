using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateMachine : MonoBehaviour
{
    public CluesState cluesState;
    public PinState pinState;

    public GameState currentState;

    // singleton
    static public GameStateMachine Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetGameState(cluesState);
    }

    private void Update()
    {
        currentState.OnStateUpdate();
    }

    public void SetGameState(GameState newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}
