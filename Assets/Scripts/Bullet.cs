using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    
    public GameObject hitEffect;
    public AudioSource shotSound;

    private bool hasExploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The if-statement prevents shotgun-pellets from exploding when colliding with each other
        // in order to prevent them from exploding immediately after being instantiated inside of each other
        if ((collision.collider.gameObject.tag == "ShotgunBullet" && tag == "ShotgunBullet") 
            || (collision.collider.gameObject.tag == "ZombieShotgunBullet" && tag == "ZombieShotgunBullet"))
        {
            return;
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Creatures"))
        {
            explodeBullet(Color.red); // Red explosion if the bullet hits an enemy target
        } 
        else
        {
            explodeBullet(Color.white); // White explosion if the bullet hits something that is not an enemy target
        }
    }

    private void Awake()
    {
        shotSound.Play();
    }

    public void explodeBullet(Color color)
    {
        hasExploded = true;
        GameObject explosionEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        explosionEffect.GetComponent<SpriteRenderer>().color = color;
        Destroy(explosionEffect, 0.3f);

        // Disables components of the bullet in order to make it unable to interact with other objects and to make it invisible
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Light2D>().enabled = false;

        // Destroys the bullet after the bullet shot sound effect has finished playing
        Destroy(gameObject, shotSound.clip.length);
        
    }

    // Explodes a bullet after a certain amount of time has passed
    // This is used for the shotgun weapon type
    public IEnumerator DelayExplosion(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null && !hasExploded)
        {
            bullet.GetComponent<Bullet>().explodeBullet(Color.white);
        }
    }

}
