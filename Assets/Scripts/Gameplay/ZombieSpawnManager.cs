using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject[] zombies;
    public float spawnTime = 5f;

    public void Start()
    {
        InvokeRepeating( nameof( SpawnZombie ), spawnTime, spawnTime );
    }

    public void SpawnZombie()
    {
        GameObject zombiePrefab = zombies[Random.Range( 0, zombies.Length )];
        Vector3 spawnPos = spawners[Random.Range( 0, spawners.Length )].position;
        spawnPos.z = zombiePrefab.transform.position.z;
        Instantiate( zombiePrefab, spawnPos, Quaternion.identity );
    }
}
