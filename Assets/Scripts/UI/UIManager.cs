using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RoundManager roundManager;
    public WeaponBase weapon;

    public void OnGUI()
    {
        GUILayout.Label( "Round: " + roundManager.round + " (Ended: " + roundManager.roundEnd + ")" );
        GUILayout.Label( "Ammo: " + weapon.ammo + "/" + weapon.clipSize );
        GUILayout.Label( "Enemies Spawned: " + roundManager.enemiesSpawned + "/" + roundManager.totalEnemiesThisRound + " (Alive: " + roundManager.aliveEnemies + "/" + roundManager.maxEnemiesAlive + ")" );
    }
}
