using UnityEngine;

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
    public string enemyTag = "Enemy";
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

    public virtual bool CanShoot()
    {
        return !isReloading && ammo > 0 && Time.time >= nextShot && !shootPointCollider.IsTouchingLayers();
    }

    public virtual void Shoot()
    {
        if( !CanShoot() ) return;

        ammo = ammo - 1;
        nextShot = Time.time + timeBetweenShots;

        RaycastHit2D shotRay = Physics2D.Raycast( shootPoint.position, shootPoint.TransformDirection( Vector2.right ), 100, raycastFilter );

        if( shotRay.transform )
        {
            if( shotRay.transform.tag == enemyTag )
            {
                Instantiate( bloodImpactParticles, shotRay.point, bloodImpactParticles.rotation );
            } else
            {
                Instantiate( objectImpactParticles, shotRay.point, objectImpactParticles.rotation );
            }
        }
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
