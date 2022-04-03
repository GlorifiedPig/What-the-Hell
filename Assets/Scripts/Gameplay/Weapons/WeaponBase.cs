using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Camera playerCam;
    public Transform playerTransform;
    public Transform weaponPivot;
    public Transform shootPoint;
    public float weaponOffset = 5f;
    public int clipSize = 10;

    private int ammo = 0;

    private void Start()
    {
        ammo = clipSize;
    }

    public virtual void Shoot()
    {
        if( ammo <= 0 ) return;
        ammo = ammo - 1;
        Debug.DrawRay( shootPoint.position, shootPoint.TransformDirection( Vector2.right ) * 25, Color.red, 1f );
    }

    public virtual void Reload()
    {
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
