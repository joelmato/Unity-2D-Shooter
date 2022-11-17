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
    private float bulletForce = 4f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        MoveTowardsPlayer();

        // Shoots if the ZombieWithPistol object is a certain distance away from the player
        if (!(Vector3.Distance(transform.position, player.transform.position) > 6))
        {
            Shoot();
        }
    }

    void MoveTowardsPlayer()
    {
        // Moves the ZombieWithPistol object towards the player if it is a certain distance away
        if (Vector3.Distance(transform.position, player.transform.position) > 6)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position; // Gets the direction of the player relative to the ZombieWithPistol object
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f; // Gets the angle between the player and the ZombieWithPistol object
        rb.rotation = angle; // Updates the angle of the ZombieWithPistol object in order to point it towards the player
    } 

    void Shoot()
    {
        if (!universalZomieScript.canAttack)
        {
            return;
        }

        muzzleFlashAnimator.SetTrigger("Start"); // Plays the muzzle flash animation

        // Creates a pistol bullet object and shoots it 
        GameObject bullet = Instantiate(pistolBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        StartCoroutine(universalZomieScript.AttackCooldown(attackCooldownTime));
    }

}
