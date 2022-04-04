using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static PowerupManager Instance;

    private void OnEnable() => Instance = this;
}
