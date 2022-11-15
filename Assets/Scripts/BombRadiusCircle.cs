using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombRadiusCircle : MonoBehaviour
{
    // List that keeps track of gameObject inside of the collider
    // (only object with the "Player" tag)
    public List<GameObject> colliderList = new List<GameObject>();

    // Adds a gameObject to the list when it enters the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliderList.Contains(collision.gameObject) && collision.CompareTag("Player"))
        {
            colliderList.Add(collision.gameObject);
        }
    }

    // Removes a gameObject to the list when it exits the collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliderList.Contains(collision.gameObject))
        {
            colliderList.Remove(collision.gameObject);
        }
    }

    // Method that deals damage to the player if it is inside the collider
    public void DamagePlayer(int bombDamage)
    {
        if (colliderList.Count == 1 && colliderList[0].CompareTag("Player"))
        {
            colliderList[0].GetComponent<Player>().TakeDamage(bombDamage);
        }
    }
}
