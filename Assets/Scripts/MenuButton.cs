using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public AudioSource hoverSoundEffect;

    // Changes the sprite of the UI text element to its highlighted version when the mouse hovers over it
    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = highlightedSprite;
        hoverSoundEffect.Play();
    }

    // Reverts the sprite of the UI text element to its normal sprite when the mouse exits it
    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = normalSprite;
    }
}
