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
        if ((collision.collider.gameObject.tag == "ShotgunBullet" && tag == "ShotgunBullet") 
            || (collision.collider.gameObject.tag == "ZombieShotgunBullet" && tag == "ZombieShotgunBullet"))
        {
            return;
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Creatures"))
        {
            explodeBullet(Color.red);
        } 
        else
        {
            explodeBullet(Color.white);
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
    public IEnumerator DelayExplosion(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        if (bullet != null && !hasExploded)
        {
            bullet.GetComponent<Bullet>().explodeBullet(Color.white);
        }
    }

}
