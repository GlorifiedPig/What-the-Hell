using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Camera playerCam;
    public Transform playerTransform;
    public Transform weaponPivot;
    public float weaponOffset = 5f;

    private void Update()
    {
        Vector3 difference = playerCam.ScreenToWorldPoint( Input.mousePosition ) - weaponPivot.position;
        float rotationZ = Mathf.Atan2( difference.y, difference.x ) * Mathf.Rad2Deg;
        weaponPivot.rotation = Quaternion.Euler( 0f, 0f, rotationZ + weaponOffset );

        Quaternion weaponRotation = transform.localRotation;
        weaponRotation.x = Mathf.Abs( rotationZ ) >= 90 ? 180 : 0;
        transform.localRotation = weaponRotation;
    }
}
