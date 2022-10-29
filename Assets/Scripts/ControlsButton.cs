using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsButton : MonoBehaviour
{
    public Sprite controlsSprite;
    public Sprite controlsSpriteHighlighted;

    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = controlsSpriteHighlighted;
    }

    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = controlsSprite;
    }
}
