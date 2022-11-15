using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombRadiusCircle : MonoBehaviour
{
    public List<GameObject> colliderList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!colliderList.Contains(collision.gameObject) && collision.CompareTag("Player"))
        {
            colliderList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliderList.Contains(collision.gameObject))
        {
            colliderList.Remove(collision.gameObject);
        }
    }

    public void DamagePlayer(int bombDamage)
    {
        if (colliderList.Count == 1 && colliderList[0].CompareTag("Player"))
        {
            colliderList[0].GetComponent<Player>().TakeDamage(bombDamage);
        }
    }
}
