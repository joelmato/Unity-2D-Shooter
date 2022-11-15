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
        yield return new WaitForSeconds(bombExplosionTimer);
        explosionSoundEffect.Play();
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 0.55f);

        bombRadiusCircle.GetComponent<BombRadiusCircle>().DamagePlayer(bombDamage);

        yield return new WaitForSeconds(explosionSoundEffect.clip.length);

        Destroy(gameObject);
    }
}
