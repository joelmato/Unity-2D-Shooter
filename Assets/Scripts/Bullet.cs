using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(effect, 0.3f);
        Destroy(gameObject, shotSound.clip.length);
    }

}
