using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    public GameObject[] enemies; 
    public Transform playerPosition;
    public GameObject player;
    
    public Transform[] spawnLocs;

    // new vars
    public int currentWave;
    public float baseSpawnRate;
    private float currentSpawnRate;
    public bool waveComplete;
    public int enemiesLeft;
    private int enemiesSpawned;
    public int[] waveSizes;
    public float rateIncrease;
    public bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        currentSpawnRate = baseSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("currwave "+ currentWave+ "  enemiesLeft "+enemiesLeft  );
        if(currentWave < waveSizes.Length-1 && waveComplete) 
        {
            SpawnWave();
        }
        if(currentWave == waveSizes.Length-1 && enemiesLeft <= 0) 
        {
            gameWon = true;
            Debug.Log("game is won");
        }
    }

    void SpawnWave() 
    {
        // Set wave variables
        currentWave += 1;
        currentSpawnRate = baseSpawnRate - currentWave * rateIncrease;
        enemiesLeft = waveSizes[currentWave];
        enemiesSpawned = 0;

        // Start enemy spawn for current wave
        Debug.Log("SpawnWave " + currentWave);
        StartCoroutine(SpawnEnemies());
    }
    
    IEnumerator SpawnEnemies() 
    {
        waveComplete = false;
        yield return new WaitForSeconds(2f);
        while(enemiesLeft > 0) 
        {
            if(enemiesSpawned < waveSizes[currentWave]) 
            {
                enemiesSpawned += 1;
                Spawn();
            }
            yield return new WaitForSeconds(currentSpawnRate);
        }
        waveComplete = true;
        yield return null;
    }

    void Spawn() 
    {
        Vector3 sloc = spawnLocs[Random.Range(0, spawnLocs.Length)].position;
        //randomly choose an enemy to spawn
        GameObject newenemy = Instantiate(enemies[Random.Range(0, enemies.Length)], sloc, Quaternion.identity);
        newenemy.SendMessage("SetTarget", playerPosition);
        newenemy.SendMessage("SetPlayer", player);
    }

    void EnemyKilled() 
    {
        Debug.Log("Enemy slain!");
        enemiesLeft -= 1;
    }
    
}
