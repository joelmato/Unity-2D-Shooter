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

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");

        healthbar = Instantiate(zombieHealthBarPrefab);
        healthbar.GetComponent<HealthBar>().SetMaxHealth(health);
        healthbar.GetComponent<HealthBar>().SetHealth(health);
        MoveHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthbar != null) MoveHealthBar();

        if (health <= 0)
        {
            if (gameObject.GetComponent<ZombieWithBomb>() != null)
            {
                gameObject.GetComponent<ZombieWithBomb>().SpawnBomb();
            }
            spawner.GetComponent<Spawner>().SpawnPowerUp(transform.position);
            Destroy(this.gameObject);

            if (healthbar != null) Destroy(healthbar);
        }
    }

    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            animator.SetTrigger("Start");
            health -= damage;
            if (healthbar != null) healthbar.GetComponent<HealthBar>().SetHealth(health);
        }
    }

    private void MoveHealthBar()
    {
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
