using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator animator;
    public CameraShake cameraShake;
    public AudioSource hurtSound;

    public int maxHealth = 100;
    public int currentHealth;

    private float shakeDuration = 0.10f;
    private float shakeMagnitude = 0.10f;
    private float invulnerabilityTime = 0.25f;

    private bool canTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the player health to its max value and updates the healthbar UI element
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        CursorController.instance.SetCrosshair();
    }

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;
        animator.SetTrigger("Start"); // Plays the hurt-animation
        hurtSound.Play();
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude)); // Shakes the camera
        StartCoroutine(WaitBeforeTakingDamage(shakeDuration, damage)); // Delays registering the damage taken until the camera shake is done
        StartCoroutine(InvulnerabilityTimer());
    }

    public void Heal(int healAmount)
    {
        if (currentHealth < 100)
        {
            animator.SetTrigger("Heal"); // Plays the healing-animation
            currentHealth += healAmount;
        }
        healthBar.SetHealth(currentHealth); // Updates the healthbar UI element
    }

    // Method for waiting a certain amount of time before dealing damage to the player
    IEnumerator WaitBeforeTakingDamage(float waitTime, int damage)
    {
        yield return new WaitForSeconds(waitTime);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth); // Updates the healthbar UI element
    }

    IEnumerator InvulnerabilityTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityTime);
        canTakeDamage = true;
    }
}
