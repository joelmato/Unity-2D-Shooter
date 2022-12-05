using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject pistolBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject rifleBulletPrefab;
    public Transform firePoint;
    public Animator muzzleFlashAnimator;

    public CameraShake cameraShake;

    public AmmoDisplay ammoDisplay;
    public ReloadingProgressBar reloadingProgressBar;

    public bool canShoot = true;
    public bool isReloading = false;
    public int currentWeapon = 0;

    // Arrays that store values for total ammo amount per magazine, current ammo amount, bullet forces,
    // reload times and firerates for all weapons { pistol, shotgun, rifle}
    private int[] ammoTotal = { 10, 4, 40 }; 
    private int[] ammoCurrent = { 10, 4, 40 }; 
    private float[] bulletForces = { 20f, 35f, 25f }; 
    private float[] reloadDurations = { 1.25f, 1.75f, 2.0f }; 
    private float[] firerates = { 0.75f, 1.5f, 0.1f }; 

    private IEnumerator shootingCooldown;
    private bool inCoroutine = false;

    private void Start()
    {
        ammoDisplay.UpdateAmmoDisplay(ammoCurrent[currentWeapon], ammoTotal[currentWeapon]);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && CheckIfCanShoot())
        {
            if (currentWeapon == 0) ShootPistol();
            if (currentWeapon == 1) ShootShotgun();
        }

        if (Input.GetButton("Fire1") && CheckIfCanShoot())
        {
            if (currentWeapon == 2) ShootRifle();
        }

        if (Input.GetButtonDown("Reload") && ammoCurrent[currentWeapon] != ammoTotal[currentWeapon] && !isReloading)
        {
            StartCoroutine(StartReloadTimer(reloadDurations[currentWeapon])); // Starts the reload timer coroutine
            reloadingProgressBar.StartTimer(reloadDurations[currentWeapon]); // Starts the reload timer animation
        }

        SwitchWeapon();
        ammoDisplay.UpdateAmmoDisplay(ammoCurrent[currentWeapon], ammoTotal[currentWeapon]);
    }

    void SwitchWeapon()
    {
        // Prevents the player from switching weapons if the weapon if reloading or the game is paused
        if (isReloading || Time.timeScale == 0f)
        {
            return;
        }

        // Changes currently equipped weapon if the player presses 1, 2, or 3
        if (Input.GetKeyDown("1") && currentWeapon != 0)
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown("2") && currentWeapon != 1)
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown("3") && currentWeapon != 2)
        {
            currentWeapon = 2;
        } 
        else
        {
            return;
        }

        ammoDisplay.UpdateAmmoSprite(currentWeapon);
        canShoot = true;
    }

    void ShootPistol()
    {
        ammoCurrent[currentWeapon]--;

        muzzleFlashAnimator.SetTrigger("Start"); // Plays the muzzle flash animation
        StartCoroutine(cameraShake.Shake(0.10f, 0.15f)); // Shakes the camera

        // Creates and shoots out a pistol bullet from the firepoint
        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForces[currentWeapon], ForceMode2D.Impulse);

        WeaponCooldown();
    }

    void ShootShotgun()
    {
        ammoCurrent[currentWeapon]--;

        muzzleFlashAnimator.SetTrigger("Start"); // Plays the muzzle flash animation
        StartCoroutine(cameraShake.Shake(0.10f, 0.2f)); // Shakes the camera

        // Creates and shoots out 7 shotgun bullets at random angles between -10 and 10 degrees from the firepoint
        for (int i = 0; i < 8; i++)
        {
            float randomAngle = Random.Range(-10f, 10f);
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 newVector = Quaternion.AngleAxis(randomAngle, new Vector3(0, 0, 1)) * firePoint.up; // Gets a vector with a random angle between -10 and 10 degrees from the firepoint
            rb.AddForce(newVector * bulletForces[currentWeapon], ForceMode2D.Impulse);

            StartCoroutine(bullet.GetComponent<Bullet>().DelayExplosion(bullet, 0.2f));
        }

        WeaponCooldown();
    }

    void ShootRifle()
    {
        ammoCurrent[currentWeapon]--;

        muzzleFlashAnimator.SetTrigger("Start"); // Plays the muzzle flash animation
        StartCoroutine(cameraShake.Shake(0.10f, 0.05f)); // Shakes the camera

        // Creates and shoots out a rifle bullet from the firepoint
        GameObject bullet = Instantiate(rifleBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForces[currentWeapon], ForceMode2D.Impulse);

        WeaponCooldown();
    }

    public IEnumerator StartReloadTimer(float time)
    {
        isReloading = true;
        // Wait until the weapon is done reloading before setting the current ammo value to the max ammo value
        yield return new WaitForSeconds(time);
        ammoCurrent[currentWeapon] = ammoTotal[currentWeapon];
        isReloading = false;
    }

    private void WeaponCooldown()
    {
        // Stops the shooting cooldown coroutine if the weapon is already in a coroutine
        // to prevent not being able to shoot after switching weapons immediately after shooting
        if (inCoroutine)
        {
            StopCoroutine(shootingCooldown);
        }

        shootingCooldown = StartShootingCooldown(firerates[currentWeapon]);
        StartCoroutine(shootingCooldown);
    }

    public IEnumerator StartShootingCooldown(float time)
    {
        canShoot = false;
        inCoroutine = true;
        yield return new WaitForSeconds(time);
        canShoot = true;
        inCoroutine = false;
    }

    private bool CheckIfCanShoot()
    {
        return (canShoot && !isReloading && ammoCurrent[currentWeapon] != 0 && Time.timeScale != 0f);
    }

}
