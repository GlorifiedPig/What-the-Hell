using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject[] zombies;

    public GameObject SpawnZombie()
    {
        GameObject zombiePrefab = zombies[Random.Range( 0, zombies.Length )];
        Vector3 spawnPos = spawners[Random.Range( 0, spawners.Length )].position;
        spawnPos.z = zombiePrefab.transform.position.z;
        return Instantiate( zombiePrefab, spawnPos, Quaternion.identity );
    }
}
