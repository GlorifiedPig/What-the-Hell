using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : WeaponBase
{
    public float slugs = 6;

    public override void Shoot()
    {
        if( !CanShoot() ) return;

        ammo = ammo - 1;
        audioSource.PlayOneShot( gunshotSounds[Random.Range( 0, gunshotSounds.Length )] );
        nextShot = Time.time + timeBetweenShots;

        for( int i = 0; i < slugs; i++ )
        {
            Vector2 direction = Vector2.right;
            direction.y += Random.Range( -spread, spread );
            RaycastHit2D shotRay = Physics2D.Raycast( shootPoint.position, shootPoint.TransformDirection( direction ), 100, raycastFilter );

            if( shotRay.transform )
            {

                Vector3 instantiatePos = shotRay.point;
                instantiatePos.z = -9f; // We want the particles to always be in front.

                if( shotRay.transform.tag == enemyTag )
                {
                    Instantiate( bloodImpactParticles, instantiatePos, bloodImpactParticles.rotation );
                    audioSource.PlayOneShot( impactSoundsFlesh[Random.Range( 0, impactSoundsFlesh.Length )], 0.15f );
                } else
                {
                    Instantiate( objectImpactParticles, instantiatePos, objectImpactParticles.rotation );
                    audioSource.PlayOneShot( impactSoundsObjects[Random.Range( 0, impactSoundsObjects.Length )], 0.15f );
                }

                float damage = Random.Range( minDamage, maxDamage );
                if( PowerupManager.activePowerup == PowerupManager.ActivePowerup.DoubleDamage ) damage = damage * 2;
                InvokeBulletHit( shotRay, damage );

                StartCoroutine( SpawnTracer( shotRay ) );
            }
        }

        if( ammo <= 0 ) Reload();
    }
}
