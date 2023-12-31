using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int bulletDamage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deals damage to the GameObject the bullet collided with if it is a zombie/enemy
        if (collision.collider.gameObject.tag == "Zombie")
        {
            collision.collider.gameObject.GetComponent<Zombie>().TakeDamage(bulletDamage);
        }
    }
}
