using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ZombieWithBomb : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public GameObject bombRadiusCircle;
    public GameObject bombPrefab;

    public AudioSource explosionSoundEffect;

    public Zombie universalZomieScript;
    public GameObject explosionEffect;

    public float movementSpeed = 2.0f;

    private float bombExplosionTimer = 2.0f;
    private bool canMove = true;
    private int bombDamage = 25;
    private bool canSpawnBomb = true;
    private bool hasCollidedWithPlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Freezes the movement of the gameObject if it collides with a player for the first time,
        // shows the bomb radius and starts the DestoyZombie coroutine
        if (collision.collider.gameObject.name == "Player" && !hasCollidedWithPlayer)
        {
            hasCollidedWithPlayer = true;
            bombRadiusCircle.SetActive(true);
            canMove = false;
            universalZomieScript.canTakeDamage = false;
            StartCoroutine(DestroyZombie());
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime); // Moves the ZombieWithBomb object towards the player

        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position; // Gets the direction of the player relative to the ZombieWithBomb object
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f; // Gets the angle between the player and the ZombieWithBomb object
        rb.rotation = angle; // Updates the angle of the ZombieWithBomb object in order to point it towards the player
    }

    IEnumerator DestroyZombie()
    {
        universalZomieScript.RemoveHealhBar();

        // Waits until the bomb timer has finished before showing the bomb explosion effect,
        // playing the sound effect, and damaging the player
        yield return new WaitForSeconds(bombExplosionTimer);
        explosionSoundEffect.Play();
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        canSpawnBomb = false;
        Destroy(explosion, 0.55f);
        bombRadiusCircle.GetComponent<BombRadiusCircle>().DamagePlayer(bombDamage);

        // Waits until the explosion sound effects has finished playing before destroying
        // the gameObject in order to prevent the audio from cutting off
        yield return new WaitForSeconds(explosionSoundEffect.clip.length);

        universalZomieScript.canTakeDamage = true;
        universalZomieScript.TakeDamage(universalZomieScript.health);
    }

    public void SpawnBomb()
    {
        if (canSpawnBomb)
        {
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }
}
