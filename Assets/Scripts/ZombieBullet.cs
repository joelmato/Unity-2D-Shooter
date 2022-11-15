using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBullet : MonoBehaviour
{
    public int bulletDamage = 25;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deals damage to the GameObject the bullet collided with if it is the player
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(bulletDamage);
        }

    }
}
