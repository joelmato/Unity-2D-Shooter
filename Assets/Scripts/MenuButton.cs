using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public AudioSource hoverSoundEffect;

    public void OnMouseEnter()
    {
        transform.GetComponent<Image>().sprite = highlightedSprite;
        hoverSoundEffect.Play();
    }

    public void OnMouseExit()
    {
        transform.GetComponent<Image>().sprite = normalSprite;
    }
}
