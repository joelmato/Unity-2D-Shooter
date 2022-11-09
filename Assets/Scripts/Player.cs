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
        hurtSound.Play();
        StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        StartCoroutine(WaitBeforeTakingDamage(shakeDuration, damage)); // Delays registering the damage taken until the camera shake is done
    }

    public void Heal(int healAmount)
    {
        if (currentHealth < 100)
        {
            animator.SetTrigger("Heal");
            currentHealth += healAmount;
        }
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator WaitBeforeTakingDamage(float waitTime, int damage)
    {
        yield return new WaitForSeconds(waitTime);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
