using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairHandler : MonoBehaviour
{
    public Image crosshair;
    public Sprite crosshairDefaultTexture;
    public Sprite crosshairDisabledTexture;

    public void SetCrosshairEnabled( bool enabled )
    {
        crosshair.sprite = enabled ? crosshairDefaultTexture : crosshairDisabledTexture;
    }

    private void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        crosshair.transform.position = Input.mousePosition;
    }
}
