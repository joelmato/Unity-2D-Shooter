using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{

    public TextMeshProUGUI ammoTotalDisplay;
    public TextMeshProUGUI ammoCurrentDisplay;

    public Image ammoSprite;
    public Sprite[] ammoSprites;

    public Sprite[] characterSprites;
    public SpriteRenderer spriteRenderer;

    public void UpdateAmmoDisplay(float ammoCurrent, float ammoTotal)
    {
        if (ammoCurrent == 0)
        {
            ammoCurrentDisplay.color = Color.red;
        }
        else
        {
            ammoCurrentDisplay.color = Color.white;
        }
        ammoTotalDisplay.text = ammoTotal.ToString();
        ammoCurrentDisplay.text = ammoCurrent.ToString();
    }

    public void UpdateAmmoSprite(int currentWeapon)
    {
        ammoSprite.sprite = ammoSprites[currentWeapon];
        spriteRenderer.sprite = characterSprites[currentWeapon];
    }
}
