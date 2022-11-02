using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWithShotgun : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject shotgunBulletPrefab;
    public Transform firePoint;
    public Animator muzzleFlashAnimator;

    public GameObject zombieHealthBarPrefab;
    private GameObject healthbar;

    private int health = 200;

    public float movementSpeed = 1.2f;
    private bool canAttack = true;
    private float attackCooldownTime = 1.5f;
    private float bulletForce = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        healthbar = Instantiate(zombieHealthBarPrefab);
        healthbar.GetComponent<HealthBar>().SetMaxHealth(health);
        healthbar.GetComponent<HealthBar>().SetHealth(health);
        MoveHealthBar();
    }

    void Update()
    {
        MoveTowardsPlayer();
        MoveHealthBar();

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Destroy(healthbar);
        }

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

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Start");
        health -= damage;
        healthbar.GetComponent<HealthBar>().SetHealth(health);
    }

    void Shoot()
    {
        if (!canAttack)
        {
            return;
        }

        muzzleFlashAnimator.SetTrigger("Start");

        for (float i = -25.0f; i <= 25.0f; i += 25.0f)
        {
            float randomAngle = Random.Range(-10f, 10f);
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 newVector = Quaternion.AngleAxis(i, new Vector3(0, 0, 1)) * firePoint.up;
            rb.AddForce(newVector * bulletForce, ForceMode2D.Impulse);

            StartCoroutine(player.GetComponent<Shooting>().DelayExplosion(bullet, 1.5f));
        }

        StartCoroutine(AttackCooldown(attackCooldownTime));
    }

    IEnumerator AttackCooldown(float attackCooldownTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }

    public IEnumerator DelayExplosion(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().explodeBullet();
        }
    }

    private void MoveHealthBar()
    {
        healthbar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }
}
