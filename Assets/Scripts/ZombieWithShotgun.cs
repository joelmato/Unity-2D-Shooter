using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

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

    private bool canMoveBackwards = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveTowardsPlayer();

        // Shoots if the ZombieWithShotgun object is a certain distance away from the player
        if (!(Vector3.Distance(transform.position, player.transform.position) > 5))
        {
            Shoot();
        }
    }
    void MoveTowardsPlayer()
    {
        // Moves the ZombieWithShotgun object towards the player if it is a certain distance away
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            // Moves the GameObject forward, toward the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 3 && canMoveBackwards)
        {
            // Moves the GameObject backward, away from the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * movementSpeed/2 * Time.deltaTime);
        }
        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position; // Gets the direction of the player relative to the ZombieWithShotgun object
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f; // Gets the angle between the player and the ZombieWithShotgun object
        rb.rotation = angle; // Updates the angle of the ZombieWithShotgun object in order to point it towards the player
    }

    // Disables backwards movement when colliding to prevent GameObject from passing through the walls of the map
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            canMoveBackwards = false;
        };
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            canMoveBackwards = true;
        }
    }

    void Shoot()
    {
        if (!universalZomieScript.canAttack)
        {
            return;
        }

        muzzleFlashAnimator.SetTrigger("Start"); // Plays the muzzle flash animation

        // Creates 3 shotgun bullets and shoots them out at -25, 0, and 25 degrees 
        for (float i = -25.0f; i <= 25.0f; i += 25.0f)
        {
            GameObject bullet = Instantiate(shotgunBulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector3 newVector = Quaternion.AngleAxis(i, new Vector3(0, 0, 1)) * firePoint.up;
            rb.AddForce(newVector * bulletForce, ForceMode2D.Impulse);

            StartCoroutine(bullet.GetComponent<Bullet>().DelayExplosion(bullet, 1.5f)); // Starts a coroutine that explodes the bullets after 1.5s have passed
        }

        StartCoroutine(universalZomieScript.AttackCooldown(attackCooldownTime));
    }
}
