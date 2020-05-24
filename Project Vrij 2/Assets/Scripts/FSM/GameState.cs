using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// source: https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/

public abstract class GameState : MonoBehaviour
{
    public virtual void OnStateUpdate() { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

}
