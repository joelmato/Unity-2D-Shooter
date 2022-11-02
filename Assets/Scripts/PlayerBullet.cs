using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int bulletDamage = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "ZombieNormal")
        {
            collision.collider.gameObject.GetComponent<ZombieNormal>().TakeDamage(bulletDamage);
        }
        else if (collision.collider.gameObject.tag == "ZombieWithPistol")
        {
            collision.collider.gameObject.GetComponent<ZombieWithPistol>().TakeDamage(bulletDamage);
        }
        else if (collision.collider.gameObject.tag == "ZombieWithShotgun")
        {
            collision.collider.gameObject.GetComponent<ZombieWithShotgun>().TakeDamage(bulletDamage);
        }
    }
}
