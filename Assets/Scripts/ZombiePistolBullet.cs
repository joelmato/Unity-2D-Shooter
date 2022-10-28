using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePistolBullet : MonoBehaviour
{
    private int bulletDamage = 25;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(bulletDamage);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
