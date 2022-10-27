using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 25;
            healthBar.SetHealth(currentHealth); 
        }*/
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Start");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
