using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeScreen : MonoBehaviour
{
    Image fadeScreen;
    
    private void Awake()
    {
        fadeScreen = GetComponent<Image>();
    }
    
    #region Fades

    // Without anim curve

    public void StartFade(Color oldColor, Color newColor, float duration)
    {
        StartCoroutine(FadeToColor(oldColor, newColor, duration));
    }

    IEnumerator FadeToColor(Color oldColor, Color newColor, float duration)
    {
        float step = 1f / duration;
        for (float i = 0f; i < 1f; i += step * Time.deltaTime)
        {
            fadeScreen.color = Color.Lerp(oldColor, newColor, i);
            yield return null;
        }

        fadeScreen.color = newColor;
    }

    // With anim curve

    public void StartFade(Color oldColor, Color newColor, float duration, AnimationCurve animCurve)
    {
        StartCoroutine(FadeToColor(oldColor, newColor, duration, animCurve));
    }

    IEnumerator FadeToColor(Color oldColor, Color newColor, float duration, AnimationCurve animCurve)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float prc = timer / duration;
            fadeScreen.color = Color.Lerp(oldColor, newColor, animCurve.Evaluate(prc));
            yield return null;
        }

        fadeScreen.color = newColor;
    }

    #endregion
}
