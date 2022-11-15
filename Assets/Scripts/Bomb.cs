using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombRadiusCircle;
    public AudioSource explosionSoundEffect;
    public GameObject explosionEffect;

    private float bombExplosionTimer = 2.0f;
    private int bombDamage = 25;

    void Start()
    {
        StartCoroutine(ExplodeBomb());
    }

    IEnumerator ExplodeBomb()
    {
        // Waits until the bomb timer has finished before showing the bomb explosion effect,
        // playing the sound effect, and damaging the player
        yield return new WaitForSeconds(bombExplosionTimer);
        explosionSoundEffect.Play();
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 0.55f);
        bombRadiusCircle.GetComponent<BombRadiusCircle>().DamagePlayer(bombDamage);

        // Waits until the sound effects has finished playing before destroying
        // the gameObject in order to prevent the audio from cutting off
        yield return new WaitForSeconds(explosionSoundEffect.clip.length); 
        Destroy(gameObject);
    }
}
