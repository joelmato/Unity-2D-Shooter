using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Sprite restartSprite;
    public Sprite restartSpriteHighlighted;

    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = restartSpriteHighlighted;
    }

    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = restartSprite;
    }
}
