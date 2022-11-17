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
    private float timeBetweenWaves = 3;

    public int numberOfHearts = 0;

    private GameObject[,] enemyWaves; // Used to store the different combinations of enemies that can be spawned together as a "wave"

    private int minutesCount = 0;
    private float secondsCount = 0;

    void Start()
    {
        // Fill enemyWaves array with different enemy combinations
        enemyWaves = new GameObject[,] {
            { zombieNormalPrefab, zombieNormalPrefab, zombieNormalPrefab, zombieNormalPrefab },
            { zombieWithPistolPrefab, zombieWithPistolPrefab, zombieWithPistolPrefab, zombieWithPistolPrefab },
            { zombieWithShotgunPrefab, zombieWithShotgunPrefab, zombieWithShotgunPrefab, zombieWithShotgunPrefab },
            { zombieNormalPrefab , zombieNormalPrefab, zombieWithPistolPrefab, zombieWithPistolPrefab },
            { zombieNormalPrefab , zombieNormalPrefab, zombieWithShotgunPrefab, zombieWithShotgunPrefab },
            { zombieWithPistolPrefab , zombieWithPistolPrefab, zombieWithShotgunPrefab, zombieWithShotgunPrefab },
            { zombieNormalPrefab , zombieNormalPrefab, zombieWithPistolPrefab, zombieWithShotgunPrefab },
            { zombieWithPistolPrefab, zombieWithPistolPrefab, zombieWithBombPrefab, zombieWithBombPrefab },
            { zombieWithShotgunPrefab, zombieWithShotgunPrefab, zombieWithBombPrefab, zombieWithBombPrefab },
            { zombieNormalPrefab, zombieWithPistolPrefab, zombieWithShotgunPrefab, zombieWithBombPrefab },
        };
    }
    void Update()
    {
        GetTime();

        if (canSpawn)
        {
            StartCoroutine(StartCooldown());
        }
    }

    // Method that spawns/instantiates random zombies at the spawn points,
    // either 4 if spawnTwoAtRandom is False or 2 if spawnTwoAtRandom is True
    void SpawnZombies(bool spawnTwoAtRandom)
    {
        int random = Random.Range(0, enemyWaves.GetLength(0) - 1);

        if (spawnTwoAtRandom)
        {
            int random2 = Random.Range(0, 1);
            GameObject zombie = Instantiate(enemyWaves[random, random2]);
            zombie.transform.position = spawnpoints[random2].transform.position;

            random2 = Random.Range(2, 3);
            GameObject zombie2 = Instantiate(enemyWaves[random, random2]);
            zombie2.transform.position = spawnpoints[random2].transform.position;
        }
        else
        {
            for (int j = 0; j < enemyWaves.GetLength(1); j++)
            {
                GameObject zombie = Instantiate(enemyWaves[random, j]);
                zombie.transform.position = spawnpoints[j].transform.position;
            }
        }
    }

    IEnumerator StartCooldown()
    {
        canSpawn = false;

        SpawnZombies(false); // Spawns 4 random zombies
        yield return new WaitForSeconds(timeBetweenWaves);

        // 2 * minutesCount number of zombies are spawned
        for (int i = 0; i < minutesCount; i++)
        {
            SpawnZombies(true); // Spawns 2 random zombies

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        // Waits until the next batch of zombies can spawn
        yield return new WaitForSeconds(spawnRate - timeBetweenWaves - minutesCount * timeBetweenWaves);
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

    // Gets the current game time in minutes and seconds
    public void GetTime()
    {
        secondsCount += Time.deltaTime;

        if (secondsCount >= 60)
        {
            secondsCount %= 60;
            minutesCount++;
        }
    }
}
