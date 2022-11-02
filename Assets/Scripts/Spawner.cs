using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnpoints;
    public GameObject zombieNormalPrefab;
    public GameObject zombieWithPistolPrefab;
    public GameObject zombieWithShotgunPrefab;

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
            //int random = Random.Range(0, spawnpoints.Length);
            GameObject zombie = Instantiate(zombieNormalPrefab);
            GameObject zombieWP = Instantiate(zombieWithPistolPrefab);
            zombie.transform.position = spawnpoints[0].transform.position;
            zombieWP.transform.position = spawnpoints[1].transform.position;

            GameObject zombie2 = Instantiate(zombieNormalPrefab);
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
}
