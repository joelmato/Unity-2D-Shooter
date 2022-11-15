using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject spawner;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject zombieHealthBarPrefab;
    private GameObject healthbar;

    public int health = 100;

    public bool canAttack = true;
    public bool canTakeDamage = true;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");

        // Creates a healthbar, updates its values and moves it to the zombie when the zombie is created/instantiated
        healthbar = Instantiate(zombieHealthBarPrefab);
        healthbar.GetComponent<HealthBar>().SetMaxHealth(health);
        healthbar.GetComponent<HealthBar>().SetHealth(health);
        MoveHealthBar();
    }

    void Update()
    {
        if (healthbar != null) MoveHealthBar();

        if (health <= 0)
        {
            // Spawns a bomb on death if the zombie is of the type "ZombieWithBomb"
            if (gameObject.GetComponent<ZombieWithBomb>() != null)
            {
                gameObject.GetComponent<ZombieWithBomb>().SpawnBomb();
            }

            spawner.GetComponent<Spawner>().SpawnPowerUp(transform.position); // Tries to spawn a power up at the location the zombie 

            Destroy(this.gameObject);
            if (healthbar != null) Destroy(healthbar);
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            animator.SetTrigger("Start"); // Plays the take-damage animation
            health -= damage;
            if (healthbar != null) healthbar.GetComponent<HealthBar>().SetHealth(health); // Updates the value of the zombies healthBar if it exists
        }
    }

    private void MoveHealthBar()
    {
        // Moves the healthBar to slightly above the zombie
        healthbar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }

    public void RemoveHealhBar()
    {
        Destroy(healthbar);
    }

    public IEnumerator AttackCooldown(float attackCooldownTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }
}
