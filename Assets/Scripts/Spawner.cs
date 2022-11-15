using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnpoints;
    public GameObject zombieNormalPrefab;
    public GameObject zombieWithPistolPrefab;
    public GameObject zombieWithShotgunPrefab;
    public GameObject zombieWithBombPrefab;

    public GameObject heartPowerUpPrefab;

    private bool canSpawn = true;
    private float spawnRate = 30;

    public int numberOfHearts = 0;

    void Update()
    {
        if (canSpawn)
        {
            SpawnZombies();
        }
    }

    // Method that spawns/instantiates zombies at the spawn points
    void SpawnZombies()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject zombie = Instantiate(zombieNormalPrefab);
            GameObject zombieWP = Instantiate(zombieWithPistolPrefab);
            zombie.transform.position = spawnpoints[0].transform.position;
            zombieWP.transform.position = spawnpoints[1].transform.position;

            GameObject zombie2 = Instantiate(zombieWithBombPrefab);
            GameObject zombieWP2 = Instantiate(zombieWithShotgunPrefab);
            zombie2.transform.position = spawnpoints[2].transform.position;
            zombieWP2.transform.position = spawnpoints[3].transform.position;
        }
        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnRate);
        canSpawn = true;
    }

    public void SpawnPowerUp(Vector3 position)
    {
        float random = Random.Range(1, 11);

        if (random < 3)
        {
            // Spawns a heartPowerUp if the number of heartPowerUps currently in the game is less than two
            if (!(numberOfHearts >= 2))
            {
                GameObject heartPowerUp = Instantiate(heartPowerUpPrefab, position, Quaternion.identity);
                numberOfHearts++;
            }
        }
    }
}
