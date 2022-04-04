
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponBase : MonoBehaviour
{
    // Configurable Fields
    public Camera playerCam;
    public Transform playerTransform;
    public Transform weaponPivot;
    public Transform shootPoint;
    public Collider2D shootPointCollider;
    public CrosshairHandler crosshairHandler;
    public LayerMask raycastFilter;
    public Transform objectImpactParticles;
    public Transform bloodImpactParticles;
    public TrailRenderer tracer;
    public string enemyTag = "Enemy";
    public int clipSize = 10;
    public float timeBetweenShots = 0.35f;
    public float reloadTime = 1.25f;
    public float minDamage = 25f;
    public float maxDamage= 50f;
    public AudioSource audioSource;
    public AudioClip[] gunshotSounds;
    public AudioClip reloadSound;
    public AudioClip[] impactSoundsObjects;
    public AudioClip[] impactSoundsFlesh;

    // Internal Fields
    public static event Action<RaycastHit2D, float> BulletHit = ( raycastHit2D, damage ) => { }; 
    public bool isReloading = false;
    public int ammo = 0;
    public float nextShot = 0f;

    private void Start()
    {
        ammo = clipSize;
    }

    public virtual bool CanShoot()
    {
        return !isReloading && ammo > 0 && Time.time >= nextShot && !shootPointCollider.IsTouchingLayers();
    }

    public virtual void Shoot()
    {
        if( !CanShoot() ) return;

        RaycastHit2D shotRay = Physics2D.Raycast( shootPoint.position, shootPoint.TransformDirection( Vector2.right ), 100, raycastFilter );

        if( shotRay.transform )
        {
            ammo = ammo - 1;
            nextShot = Time.time + timeBetweenShots;

            audioSource.PlayOneShot( gunshotSounds[Random.Range( 0, gunshotSounds.Length - 1 )] );
        
            Vector3 instantiatePos = shotRay.point;
            instantiatePos.z = -9f; // We want the particles to always be in front.

            if( shotRay.transform.tag == enemyTag )
            {
                Instantiate( bloodImpactParticles, instantiatePos, bloodImpactParticles.rotation );
                audioSource.PlayOneShot( impactSoundsFlesh[Random.Range( 0, impactSoundsFlesh.Length - 1 )], 0.4f );
            } else
            {
                Instantiate( objectImpactParticles, instantiatePos, objectImpactParticles.rotation );
                audioSource.PlayOneShot( impactSoundsObjects[Random.Range( 0, impactSoundsObjects.Length - 1 )], 0.4f );
            }

            BulletHit.Invoke( shotRay, Random.Range( minDamage, maxDamage ) );

            StartCoroutine( SpawnTracer( shotRay ) );
        }
    }

    public IEnumerator SpawnTracer( RaycastHit2D hit )
    {
        float time = 0f;
        Vector3 startPosition = shootPoint.position;
        TrailRenderer trail = Instantiate( tracer, startPosition, Quaternion.identity );

        while( time < tracer.time )
        {
            trail.transform.position = Vector3.Lerp( startPosition, hit.point, time );
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hit.point;
        Destroy( trail.gameObject, trail.time );
    }

    public virtual void Reload()
    {
        if( isReloading ) return;

        audioSource.PlayOneShot( reloadSound );
        isReloading = true;
        Invoke( nameof( FinishReload ), reloadTime );
    }

    public virtual void FinishReload()
    {
        if( !isReloading ) return;

        isReloading = false;
        ammo = clipSize;
        CancelInvoke();
    }

    public void HandleControls()
    {
        if( Input.GetMouseButtonDown( 0 ) ) Shoot();
        if( Input.GetKeyDown( KeyCode.R ) ) Reload();
    }

    public void HandleRotation()
    {
        Vector3 difference = playerCam.ScreenToWorldPoint( Input.mousePosition ) - weaponPivot.position;
        float rotationZ = Mathf.Atan2( difference.y, difference.x ) * Mathf.Rad2Deg;
        weaponPivot.rotation = Quaternion.Euler( 0f, 0f, rotationZ );

        Quaternion weaponRotation = transform.localRotation;
        weaponRotation.x = Mathf.Abs( rotationZ ) >= 90 ? 180 : 0;
        transform.localRotation = weaponRotation;
    }

    public void HandleWeaponCrosshair()
    {
        crosshairHandler.SetCrosshairEnabled( CanShoot() );
    }

    private void Update()
    {
        HandleWeaponCrosshair();
        HandleControls();
        HandleRotation();
    }

    // Debug IMGUI to display weapon statistics.
    private void OnGUI()
    {
        GUILayout.Label( "Ammo: " + ammo + "/" + clipSize );
    }
}
