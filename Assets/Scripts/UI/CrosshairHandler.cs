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
        Cursor.lockState = CursorLockMode.None;
        if( Player.alive )
        {
            Cursor.visible = false;
            crosshair.transform.position = Input.mousePosition;
        } else
        {
            Cursor.visible = true;
        }
    }
}
