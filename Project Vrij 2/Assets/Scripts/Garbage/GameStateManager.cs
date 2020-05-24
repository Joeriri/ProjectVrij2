using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameStates
    {
        Researching,
        Pinning,
        Form,
        Selecting
    }
    public GameStates state;

    public GameStateManager Instance;

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

    public void SetState(GameStates newState)
    {
        state = newState;
    }
}
