using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;

    public Zombie universalZomieScript;

    public float movementSpeed = 1.5f;
    private float attackCooldownTime = 2.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player" && universalZomieScript.canAttack)
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(25);
            StartCoroutine(universalZomieScript.AttackCooldown(attackCooldownTime));
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }


}
