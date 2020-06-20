using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] List<UnityEvent> events = new List<UnityEvent>();

    public void InvokeEvent(int _index)
    {
        events[_index].Invoke();
    }
}
