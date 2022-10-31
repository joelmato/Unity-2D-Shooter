using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWithPistol : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject pistolBulletPrefab;
    public Transform firePoint;
    public Animator muzzleFlashAnimator;

    private int health = 150;

    public float movementSpeed = 1.0f;
    private bool canAttack = true;
    private float attackCooldownTime = 0.75f;
    private float bulletForce = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

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

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Start");
        health -= damage;
    }

    void Shoot()
    {
        if (!canAttack)
        {
            return;
        }

        muzzleFlashAnimator.SetTrigger("Start");

        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        StartCoroutine(AttackCooldown(attackCooldownTime));
    }

    IEnumerator AttackCooldown(float attackCooldownTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }
}
