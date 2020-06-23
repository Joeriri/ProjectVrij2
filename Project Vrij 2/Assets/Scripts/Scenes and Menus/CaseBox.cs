using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class CaseBox : MonoBehaviour
{
    public bool isInteractable = true;

    [Header("Outline Animation")]
    [SerializeField] private Color outlineColor = Color.white;
    [SerializeField] private AnimationCurve outlineAnimationCurve;
    [SerializeField] private float outlineAnimationDuration = 1f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartOutlineAnimation();
    }

    private void OnMouseDown()
    {
        if (isInteractable)
        {
            MouseCursor.Instance.SetCursor("click");
        }
    }

    private void OnMouseUp()
    {
        if (isInteractable)
        {
            MouseCursor.Instance.SetCursor("point");
        }
    }

    private void OnMouseUpAsButton()
    {
        if (isInteractable)
        {
            // stop outline
            StopAllCoroutines();
            SetOutlineColor(Color.clear);
            // open letter
            Intro.Instance.OnCaseBoxClicked();
            isInteractable = false;
            // Play sound
            FMODUnity.RuntimeManager.PlayOneShot("event:/Form");

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
