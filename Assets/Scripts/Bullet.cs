using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    
    public GameObject hitEffect;
    public AudioSource shotSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "ShotgunBullet" && tag == "ShotgunBullet")
        {
            return;
        }

        explodeBullet();

    }

    private void Awake()
    {
        shotSound.Play();
    }

    public void explodeBullet()
    {
        GameObject explosionEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 0.3f);

        // Disables components of the bullet in order to make it unable to interact with other objects and to make it invisible
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Light2D>().enabled = false;

        // Destroys the bullet after the bullet shot sound effect has finished playing
        Destroy(gameObject, shotSound.clip.length);
    }

}
