
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public enum ActivePowerup
    {
        None,
        DoubleDamage,
        Speed
    }

    public Player player;
    public AudioSource audioSource;
    public float powerupChance = 0.1f;

    public static PowerupManager Instance;
    public static ActivePowerup activePowerup = ActivePowerup.None;

    public GameObject healthPowerup;
    public GameObject armorPowerup;
    public GameObject doubleDamagePowerup;
    public GameObject speedPowerup;

    private void OnEnable() {
        Instance = this;
        Enemy.EnemyDeath += EnemyDeath;
    }

    private void OnDisable()
    {
        Enemy.EnemyDeath -= EnemyDeath;
    }

    public void MakeActivePowerup( ActivePowerup powerup, float length )
    {
        if( activePowerup != ActivePowerup.None ) return;
        activePowerup = powerup;
        Invoke( nameof( ClearActivePowerup ), length );
    }

    public void ClearActivePowerup()
    {
        activePowerup = ActivePowerup.None;
    }

    public void SpawnPowerup( Vector2 position )
    {
        List<GameObject> powerupPool = new List<GameObject>();

        if( player.health < 100 ) powerupPool.Add( healthPowerup );
        if( player.armor < 100 ) powerupPool.Add( armorPowerup );
        if( activePowerup == ActivePowerup.None )
        {
            powerupPool.Add( doubleDamagePowerup );
            powerupPool.Add( speedPowerup );
        }

        if( powerupPool.Count > 0 ) Instantiate( powerupPool[Random.Range( 0, powerupPool.Count )], position, Quaternion.identity );
    }

    public void EnemyDeath( Enemy enemy )
    {
        if( Random.Range( 0f, 1f ) > powerupChance ) return;
        SpawnPowerup( enemy.transform.position );
    }
}
