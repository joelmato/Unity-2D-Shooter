using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;

    public bool isAvailable = true;
    public int currentWeapon = 1;

    public float bulletForcePistol = 20f;
    public float bulletForceShotgun = 35f;
    public float bulletForceRifle = 25f;

    public float CooldownDurationPistol = 0.75f;
    public float CooldownDurationShotgun = 1.5f;
    public float CooldownDurationRifle = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentWeapon == 1) ShootPistol();
            if (currentWeapon == 2) ShootShotgun();
        }

        SwitchWeapon();
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown("1"))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown("3"))
        {
            currentWeapon = 3;
        }
    }

    void ShootPistol()
    {
        if (!isAvailable)
        {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForcePistol, ForceMode2D.Impulse);

        StartCoroutine(StartCooldown(CooldownDurationPistol));
    }

    void ShootShotgun()
    {
        if (!isAvailable)
        {
            return;
        }

        for (int i = 0; i < 8; i++)
        {
            float randomAngle = Random.Range(-10f, 10f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 newVector = Quaternion.AngleAxis(randomAngle, new Vector3(0, 0, 1)) * firePoint.up;
            rb.AddForce(newVector * bulletForcePistol, ForceMode2D.Impulse);

            StartCoroutine(DelayExplosion(bullet, 0.2f));
        }

        StartCoroutine(StartCooldown(CooldownDurationShotgun));
    }

    public IEnumerator StartCooldown(float coolDown)
    {
        isAvailable = false;
        yield return new WaitForSeconds(coolDown);
        isAvailable = true;
    }

    public IEnumerator DelayExplosion(GameObject bullet, float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().explodeBullet();
        }
    }


}
