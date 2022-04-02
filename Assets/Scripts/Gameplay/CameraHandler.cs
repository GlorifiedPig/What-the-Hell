using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform player;
    public float smoothingSpeed = 0.125f;

    private void Update()
    {
        Vector3 position = player.position;
        position.z = transform.position.z;
        transform.position = position;
    }
}
