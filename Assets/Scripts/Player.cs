using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator animator;
    public CameraShake cameraShake;

    public int maxHealth = 100;
    public int currentHealth;

    private float shakeDuration = 0.10f;
    private float shakeMagnitude = 0.10f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        CursorController.instance.SetCrosshair();
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Start");
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        StartCoroutine(WaitBeforeTakingDamage(shakeDuration, damage)); // Delays registering the damage taken until the camera shake is done
    }

    IEnumerator WaitBeforeTakingDamage(float waitTime, int damage)
    {
        yield return new WaitForSeconds(waitTime);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
