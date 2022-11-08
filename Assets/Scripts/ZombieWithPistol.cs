using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWithPistol : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;

    public GameObject pistolBulletPrefab;
    public Transform firePoint;
    public Animator muzzleFlashAnimator;

    public Zombie universalZomieScript;

    public float movementSpeed = 1.0f;
    private float attackCooldownTime = 0.75f;
    private float bulletForce = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveTowardsPlayer();

        if (!(Vector3.Distance(transform.position, player.transform.position) > 6))
        {
            Shoot();
        }
    }

    void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 6)
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

        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        StartCoroutine(universalZomieScript.AttackCooldown(attackCooldownTime));
    }

}
