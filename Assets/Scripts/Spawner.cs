using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnpoints;
    public GameObject zombieNormalPrefab;

    private bool canSpawn = true;
    private float spawnRate = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            SpawnZombies();
        }
    }

    void SpawnZombies()
    {

        for (int i = 0; i < 1; i++)
        {
            int random = Random.Range(0, spawnpoints.Length);
            GameObject zombie = Instantiate(zombieNormalPrefab);
            zombie.transform.position = spawnpoints[random].transform.position;
        }
        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    }
}
