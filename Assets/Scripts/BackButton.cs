using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public Sprite backSprite;
    public Sprite backSpriteHighlighted;

    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = backSpriteHighlighted;
    }

    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = backSprite;
    }
}
