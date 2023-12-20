using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public float spawnRadius = 80f;
    public LayerMask groundLayer;
    public float enemySpawnCooldown = 10f;
    private float lastSpawnTime;

    public Timer t;
    public GameObject player;

    List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        lastSpawnTime = t.GetTime(); // Initialize lastSpawnTime
        gameObjects.Add(enemy1);
        gameObjects.Add(enemy2);
    }

    void Update()
    {
        if (ShouldSpawnEnemy())
        {
            SpawnEnemy();
        }
    }

    bool ShouldSpawnEnemy()
    {
        // Check cooldown
        if (t.GetTime() - lastSpawnTime < enemySpawnCooldown)
        {
            return false;
        }

        // Check if the player is not looking in the direction of the spawn point
        Vector3 playerDirection = player.transform.forward;
        Vector3 spawnPointDirection = transform.position - player.transform.position;
        float angle = Vector3.Angle(playerDirection, spawnPointDirection);

        if (angle > 90f)
        {
            // Check if there is ground where the enemy will spawn
            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);
            if (Physics.Raycast(ray, 1f, groundLayer))
            {
                return true;
            }
        }

        return false;
    }

    void SpawnEnemy()
    {
        // Update last spawn time
        lastSpawnTime = t.GetTime();

        // Calculate a point behind the player's look direction
        Vector3 spawnDirection = -player.transform.forward;
        Vector3 spawnPosition = player.transform.position + spawnDirection * spawnRadius;

        // Make sure the enemy is above the ground
        RaycastHit hit;
        if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out hit, 100f, groundLayer))
        {
            spawnPosition = hit.point + Vector3.up * 0.5f;
        }

        // Offset the spawn position in the y direction
        spawnPosition += Vector3.up;

        if (t.GetTime() > 30f)
        {
            Instantiate(gameObjects[Mathf.RoundToInt(Random.value)], spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(gameObjects[0], spawnPosition, Quaternion.identity);
        }
    }
}
