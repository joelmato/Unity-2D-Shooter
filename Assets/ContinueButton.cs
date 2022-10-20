using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public Sprite continueSprite;
    public Sprite continueSpriteHighlighted;

    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = continueSpriteHighlighted;
    }

    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = continueSprite;
    }
}
