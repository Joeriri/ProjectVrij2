using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseBox : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    [Header("Outline ANimation")]
    [SerializeField] private Color outlineColor = Color.white;
    //[Range(0f, 1f)] public float outlineAlpha = 1f;
    [SerializeField] private AnimationCurve outlineAnimationCurve;
    [SerializeField] private float outlineAnimationDuration = 1f;

    private bool hasbeenClicked = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartOutlineAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (!hasbeenClicked)
        {
            MouseCursor.Instance.SetCursor("click");
        }
    }

    private void OnMouseUp()
    {
        if (!hasbeenClicked)
        {
            MouseCursor.Instance.SetCursor("point");
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!hasbeenClicked)
        {
            // stop outline
            StopAllCoroutines();
            SetOutlineColor(Color.clear);
            // open letter
            Intro.Instance.OpenLetter();
            hasbeenClicked = true;
            // Play sound
            // Freek: play sound

            MouseCursor.Instance.SetCursor("point");
        }
    }

    #region Outline Animation

    void StartOutlineAnimation()
    {
        StartCoroutine(outlineAnim(outlineAnimationDuration));
    }

    IEnumerator outlineAnim(float duration)
    {
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float prc = timer / duration;
            SetOutlineColor(new Color(outlineColor.r, outlineColor.g, outlineColor.b, outlineAnimationCurve.Evaluate(prc)));

            yield return null;
        }

        StartOutlineAnimation();
    }

    // outline shader source: https://assetstore.unity.com/packages/vfx/shaders/2d-sprite-outline-109669#reviews
    // source for setting shader property: https://nielson.dev/2016/04/2d-sprite-outlines-in-unity
    void SetOutlineColor(Color newColor)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetColor("_SolidOutline", newColor);
        spriteRenderer.SetPropertyBlock(mpb);
    }

    #endregion
}
