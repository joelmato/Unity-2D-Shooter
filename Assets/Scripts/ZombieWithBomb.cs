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

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        Vector2 lookDirection = player.GetComponent<Rigidbody2D>().position - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
    }

    IEnumerator DestroyZombie()
    {
        universalZomieScript.RemoveHealhBar();
        yield return new WaitForSeconds(bombExplosionTimer);
        explosionSoundEffect.Play();
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        canSpawnBomb = false;
        Destroy(explosion, 0.55f);

        bombRadiusCircle.GetComponent<BombRadiusCircle>().DamagePlayer(bombDamage);

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
