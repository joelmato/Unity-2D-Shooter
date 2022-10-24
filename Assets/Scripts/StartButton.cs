using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Sprite startSprite;
    public Sprite startSpriteHighlighted;

    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = startSpriteHighlighted;
    }

    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = startSprite;
    }
}
