using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public TextMeshProUGUI ammoTotalDisplay;
    public TextMeshProUGUI ammoCurrentDisplay;
    public TextMeshProUGUI reloadingDisplay;
    public Image ammoSprite;
    public Sprite[] ammoSprites;

    private bool isAvailable = true;
    private bool isReloading = false;
    public int currentWeapon = 0;

    private float bulletForcePistol = 20f;
    private float bulletForceShotgun = 35f;
    private float bulletForceRifle = 25f;

    public int[] ammoTotal = { 10, 4, 20 }; // Stores all values for total ammo amount per magazine for all weapons, { pistol, shotgun, rifle}
    public int[] ammoCurrent = { 10, 4, 20 }; // Stores all values for current ammo amount for all weapons, { pistol, shotgun, rifle}
    public float[] reloadDurations = { 2.0f, 2.5f, 4.0f }; // Stores all values for reload times for all weapons, { pistol, shotgun, rifle}
    public float[] firerates = { 0.75f, 1.5f, 0.5f }; // Stores all values for time between shots for all weapons, { pistol, shotgun, rifle}



    private void Start()
    {
        UpdateAmmoDisplay();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon == 0) ShootPistol();
            if (currentWeapon == 1) ShootShotgun();
        }

        if (Input.GetButtonDown("Reload") && ammoCurrent[currentWeapon] != ammoTotal[currentWeapon])
        {
            Reload();
        }

        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        if (!isAvailable || isReloading)
        {
            return;
        }

        if (Input.GetKeyDown("1"))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown("2"))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown("3"))
        {
            currentWeapon = 2;
        }
        ammoSprite.sprite = ammoSprites[currentWeapon];
        UpdateAmmoDisplay();
    }

    void ShootPistol()
    {
        if (!isAvailable || isReloading || ammoCurrent[0] == 0)
        {
            return;
        }

        ammoCurrent[currentWeapon]--;
        UpdateAmmoDisplay();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForcePistol, ForceMode2D.Impulse);

        StartCoroutine(StartCooldown(firerates[currentWeapon]));
    }

    void ShootShotgun()
    {
        if (!isAvailable || isReloading || ammoCurrent[1] == 0)
        {
            return;
        }

        ammoCurrent[currentWeapon]--;
        UpdateAmmoDisplay();

        for (int i = 0; i < 8; i++)
        {
            float randomAngle = Random.Range(-10f, 10f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 newVector = Quaternion.AngleAxis(randomAngle, new Vector3(0, 0, 1)) * firePoint.up;
            rb.AddForce(newVector * bulletForceShotgun, ForceMode2D.Impulse);

            StartCoroutine(DelayExplosion(bullet, 0.2f));
        }

        StartCoroutine(StartCooldown(firerates[currentWeapon]));
    }

    void Reload()
    {
        StartCoroutine(StartReloadTimer(reloadDurations[currentWeapon]));
    }


    void UpdateAmmoDisplay()
    {
        if (ammoCurrent[currentWeapon] == 0)
        {
            ammoCurrentDisplay.color = Color.red;
        } else
        {
            ammoCurrentDisplay.color = Color.white;
        }
        ammoTotalDisplay.text = ammoTotal[currentWeapon].ToString();
        ammoCurrentDisplay.text = ammoCurrent[currentWeapon].ToString();
    }

    public IEnumerator StartReloadTimer(float time)
    {
        reloadingDisplay.text = "Reloading";
        isReloading = true;
        yield return new WaitForSeconds(time);
        ammoCurrent[currentWeapon] = ammoTotal[currentWeapon];
        isReloading = false;
        reloadingDisplay.text = "";
        UpdateAmmoDisplay();
    }

    public IEnumerator StartCooldown(float time)
    {
        isAvailable = false;
        yield return new WaitForSeconds(time);
        isAvailable = true;
    }

    public IEnumerator DelayExplosion(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().explodeBullet();
        }
    }


}
