using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int initialAmount = 3;
    public float timeBetweenAdditionalSpawns = 3f;
    public float timeVariance = 1f;
    [Tooltip("How many objects to spawn at once. 0 if you want no more spawns")]
    public int spawnVolume = 1;
    [Tooltip("Chance of spawning an object. 0 - 100%")]
    [Range(0, 100)]
    public int chanceOfSpawn = 100;

    public Vector2 spawnAreaSize = new Vector2(30,30);
    public Color gizmoColor = Color.green;

    void Start()
    {
        // Initial Spawn
        for (int i = 0; i < initialAmount; i++)
        {
            Spawn();
        }

        // Set off the spawning chain
        QueueNextSpawn();
    }

    public void QueueNextSpawn()
    {
        float randomTime = Random.Range(timeBetweenAdditionalSpawns - timeVariance, timeBetweenAdditionalSpawns + timeVariance);
        Invoke("Spawn", randomTime);

        // If we're spawning 1 or more objects after the intial, queue the next spawn
        if (spawnVolume > 0)
        {
            // Queue the next to be spawned
            Invoke("QueueNextSpawn", randomTime);
        }
    }

    public void Spawn()
    {
        //Debug.Log("Spawning");
        if (chanceOfSpawn != 100)
        {
            // If there isn't a 100% chance of spawning
            // Check for chance of spawn
            int randomSpawnChance = Random.Range(0, 100);
            if (randomSpawnChance > chanceOfSpawn)
            {
                //Debug.Log("Not spawning this time");
                return;
            }
        }

        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            0,
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        //Debug.Log("Spawning at " + randomPosition);

        Instantiate(prefab, transform.position + randomPosition, Quaternion.identity);
    }

    // Draw the spawn area in the Scene view
    void OnDrawGizmos()
    {
        Vector3 displayArea = new Vector3(spawnAreaSize.x, 0, spawnAreaSize.y);
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, displayArea);
    }
}
