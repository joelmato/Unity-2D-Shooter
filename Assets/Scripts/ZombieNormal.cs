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
        // Deals damage to the GameObject the zombie collided with if it is the player and the attack is not on cooldown
        if (collision.collider.gameObject.name == "Player" && universalZomieScript.canAttack)
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(25); // Deal damage to the player
            StartCoroutine(universalZomieScript.AttackCooldown(attackCooldownTime)); // Starts the attack cooldown
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime); // Moves the ZombieNormal object towards the player

        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position; // Gets the direction of the player relative to the ZombieNormal object
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f; // Gets the angle between the player and the ZombieNormal object
        rb.rotation = angle; // Updates the angle of the ZombieNormal object in order to point it towards the player
    }
}
