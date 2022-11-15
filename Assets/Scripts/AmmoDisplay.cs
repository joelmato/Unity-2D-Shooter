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

    // Updates the ammo counter display on the player HUD
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

    // Updates the ammo sprites on the player HUD to represent the equipped weapon
    public void UpdateAmmoSprite(int currentWeapon)
    {
        ammoSprite.sprite = ammoSprites[currentWeapon];
        spriteRenderer.sprite = characterSprites[currentWeapon];
    }
}
