using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWithShotgun : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;

    public Zombie universalZomieScript;

    public GameObject shotgunBulletPrefab;
    public Transform firePoint;
    public Animator muzzleFlashAnimator;

    public float movementSpeed = 1.2f;
    private float attackCooldownTime = 1.5f;
    private float bulletForce = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveTowardsPlayer();

        if (!(Vector3.Distance(transform.position, player.transform.position) > 5))
        {
            Shoot();
        }
    }
    void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }


    void Shoot()
    {
        if (!universalZomieScript.canAttack)
        {
            return;
        }

        muzzleFlashAnimator.SetTrigger("Start");

        for (float i = -25.0f; i <= 25.0f; i += 25.0f)
        {
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 newVector = Quaternion.AngleAxis(i, new Vector3(0, 0, 1)) * firePoint.up;
            rb.AddForce(newVector * bulletForce, ForceMode2D.Impulse);

            StartCoroutine(bullet.GetComponent<Bullet>().DelayExplosion(bullet, 1.5f));
        }

        StartCoroutine(universalZomieScript.AttackCooldown(attackCooldownTime));
    }
}
