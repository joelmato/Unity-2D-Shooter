using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject zombieHealthBarPrefab;
    private GameObject healthbar;

    private int health = 100;

    public float movementSpeed = 1.5f;
    private bool canAttack = true;
    private float attackCooldownTime = 2.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        healthbar = Instantiate(zombieHealthBarPrefab);
        healthbar.GetComponent<HealthBar>().SetMaxHealth(health);
        healthbar.GetComponent<HealthBar>().SetHealth(health);
        MoveHealthBar();

    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        MoveHealthBar();

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Destroy(healthbar);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player" && canAttack)
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(25);
            StartCoroutine(AttackCooldown());
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

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

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }

    private void MoveHealthBar()
    {
        healthbar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }

}
