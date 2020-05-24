using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    public Sprite handPointSprite;
    public Sprite handPressSprite;
    public Sprite handDragSprite;

    private Sprite point;
    private Sprite press;
    private Sprite drag;

    private Image cursorImage;

    static public MouseCursor Instance;

    private void Awake()
    {
        Instance = this;

        cursorImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Input.mousePosition;
    }

    public void SetCursor(string name)
    {
        if (name == "point") cursorImage.sprite = handPointSprite;
        if (name == "click") cursorImage.sprite = handPressSprite;
        if (name == "hold") cursorImage.sprite = handDragSprite;
    }

    public void SetCursor(Sprite newSprite)
    {
        cursorImage.sprite = newSprite;
    }
}
