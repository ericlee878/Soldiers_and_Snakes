using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// Object to spawn
    public GameObject SnakePrefab;

    /// Seconds between spawn operations
    public float SpawnInterval = 3.0f;

    /// When the next enemy should be spawned
    public float nextSpawnTime = 0;

    public Soldier soldier;

    public UnityEngine.Vector2 SpawnPoint = new UnityEngine.Vector2(10.0f, -5.0f);


    // Start is called before the first frame update
    void Start()
    {
        soldier = FindObjectOfType<Soldier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!soldier.isDead && Time.time > nextSpawnTime)
        {
            // Instantiate an enemy at the random spawn point.
            Instantiate(SnakePrefab, SpawnPoint, UnityEngine.Quaternion.identity);

            // Set the next spawn time by adding the SpawnInterval to the current time.
            nextSpawnTime = Time.time + SpawnInterval;
        }
    }
}
