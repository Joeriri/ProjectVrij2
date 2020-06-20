using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Fade
{
    public Color startColor = Color.black;
    public Color endColor = Color.clear;
    public AnimationCurve fadeCurve = new AnimationCurve();
    public float duration = 1f;
}
