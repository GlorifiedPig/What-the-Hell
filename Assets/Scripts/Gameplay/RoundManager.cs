
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public AudioSource gameAudioSource;
    public AudioClip gameStartSound;
    public int round = 0;
    public int maxEnemiesAlive = 24;
    public float spawnTimer = 5f;
    public float timeBetweenRounds = 8f;
    public ZombieSpawnManager zombieSpawnManager;

    public bool roundEnd = true;
    public int aliveEnemies = 0;
    public int enemiesSpawned = 0;
    public int totalEnemiesThisRound = 0;

    private void OnEnable() => Enemy.EnemyDeath += EnemyDeath;
    private void OnDisable() => Enemy.EnemyDeath -= EnemyDeath;

    private void Start()
    {
        InvokeRepeating( nameof( SpawnCheck ), timeBetweenRounds, spawnTimer );
        Invoke( nameof( StartRound ), timeBetweenRounds );
    }

    public void StartRound()
    {
        round = round + 1;
        if( round == 1 ) gameAudioSource.PlayOneShot( gameStartSound );
        roundEnd = false;
        enemiesSpawned = 0;
        totalEnemiesThisRound = Mathf.RoundToInt( (float)( 0.00005 * Mathf.Pow( round, 3.45f ) + 0.05 * Mathf.Pow( round, 2 ) + 0.5 * round + 12 ) );
    }

    public void EndRound()
    {
        roundEnd = true;
        Invoke( nameof( StartRound ), timeBetweenRounds );
    }

    public void SpawnCheck()
    {
        if( roundEnd || enemiesSpawned >= totalEnemiesThisRound || aliveEnemies >= maxEnemiesAlive || !Player.alive ) return;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        enemiesSpawned += 1;
        aliveEnemies += 1;
        Enemy enemy = zombieSpawnManager.SpawnZombie().GetComponent<Enemy>();
        enemy.SetHealth( enemy.baseHealth + ( ( round - 1 ) * enemy.healthPerRound ) );
    }

    public void EnemyDeath( Enemy enemy )
    {
        aliveEnemies -= 1;
        if( aliveEnemies <= 0 && enemiesSpawned >= totalEnemiesThisRound ) EndRound();
    }
}
