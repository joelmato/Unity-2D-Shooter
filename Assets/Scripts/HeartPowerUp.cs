using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HeartPowerUp : MonoBehaviour
{
    public int healingAmount = 25;
    public AudioSource pickupSound;
    private GameObject spawner;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Picks up the HeartPowerUp if the GameObject colliding with the power up is the player
        if (collision.CompareTag("Player"))
        {
            PickUp(collision);
        }
    }

    private void PickUp(Collider2D collision)
    {
        // Returns if the player has full health in order to prevent the player from "wasting" the pickup
        if (collision.GetComponent<Player>().currentHealth == 100)
        {
            return;
        }

        pickupSound.Play();
        collision.GetComponent<Player>().Heal(healingAmount); // Heals the player
        spawner.GetComponent<Spawner>().numberOfHearts--;

        // Disables components of the HeartPowerUp in order to make it unable to interact with other objects and to make it invisible
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Light2D>().enabled = false;

        // Destroys the HeartPowerUp after the pickUp sound effect has finished playing
        Destroy(gameObject, pickupSound.clip.length);
    }
}
