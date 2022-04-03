using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    // Configurable Fields
    public Camera playerCam;
    public Transform playerTransform;
    public Transform weaponPivot;
    public Transform shootPoint;
    public float weaponOffset = 5f;
    public int clipSize = 10;
    public float timeBetweenShots = 0.35f;
    public float reloadTime = 1.25f;

    // Internal Fields
    public bool isReloading = false;
    public int ammo = 0;
    public float nextShot = 0f;

    private void Start()
    {
        ammo = clipSize;
    }

    public virtual void Shoot()
    {
        if( isReloading || ammo <= 0 || Time.time < nextShot ) return;

        ammo = ammo - 1;
        nextShot = Time.time + timeBetweenShots;

        Debug.DrawRay( shootPoint.position, shootPoint.TransformDirection( Vector2.right ) * 25, Color.red, 1f );
    }

    public virtual void Reload()
    {
        if( isReloading ) return;

        isReloading = true;
        Invoke( nameof( FinishReload ), reloadTime );
    }

    public virtual void FinishReload()
    {
        if( !isReloading ) return;

        isReloading = false;
        ammo = clipSize;
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
        weaponPivot.rotation = Quaternion.Euler( 0f, 0f, rotationZ + weaponOffset );

        Quaternion weaponRotation = transform.localRotation;
        weaponRotation.x = Mathf.Abs( rotationZ ) >= 90 ? 180 : 0;
        transform.localRotation = weaponRotation;
    }

    private void Update()
    {
        HandleControls();
        HandleRotation();
    }

    // Debug IMGUI to display weapon statistics.
    private void OnGUI()
    {
        GUILayout.Label( "Ammo: " + ammo + "/" + clipSize );
    }
}
