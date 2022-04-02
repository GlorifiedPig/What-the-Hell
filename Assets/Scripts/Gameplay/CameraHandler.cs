using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public Transform shootPoint;
    public float smoothingSpeed = 0.2f;
    public float maxDistance = 15f;
    public float offsetZ = -10;

    private Vector3 smoothingVelocity;
    private Vector3 targetPosition;

    private void ShootWeapon()
    {
        Debug.DrawRay( shootPoint.position, shootPoint.TransformDirection( Vector2.right ) * 25, Color.red, 1f );
    }

    private void Update()
    {
        if( Input.GetMouseButtonDown( 0 ) ) ShootWeapon();

        Vector3 mousePos = cam.ScreenToWorldPoint( Input.mousePosition );
        Vector3 position = ( player.position + mousePos ) / 2f;

        position.x = Mathf.Clamp( position.x, -maxDistance + player.position.x, maxDistance + player.position.x );
        position.y = Mathf.Clamp( position.y, -maxDistance + player.position.y, maxDistance + player.position.y );

        position.z = offsetZ;
        targetPosition = position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref smoothingVelocity, smoothingSpeed );
    }
}
