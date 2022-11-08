using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public Animator animator;

    public GameObject zombieHealthBarPrefab;
    private GameObject healthbar;

    public int health = 100;

    public bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        healthbar = Instantiate(zombieHealthBarPrefab);
        healthbar.GetComponent<HealthBar>().SetMaxHealth(health);
        healthbar.GetComponent<HealthBar>().SetHealth(health);
        MoveHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHealthBar();

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Destroy(healthbar);
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Start");
        health -= damage;
        healthbar.GetComponent<HealthBar>().SetHealth(health);
    }

    private void MoveHealthBar()
    {
        healthbar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
    }
    public IEnumerator AttackCooldown(float attackCooldownTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime);
        canAttack = true;
    }
}
