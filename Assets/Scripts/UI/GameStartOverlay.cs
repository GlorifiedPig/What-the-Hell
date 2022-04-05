using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartOverlay : MonoBehaviour
{
    public GameObject startOverlay;
    public static bool startOverlayOpen = true;

    public void Start()
    {
        Time.timeScale = 0;
        startOverlay.SetActive( true );
        startOverlayOpen = true;
    }

    public void CloseStartOverlay()
    {
        if( !startOverlayOpen ) return;
        Time.timeScale = 1;
        startOverlay.SetActive( false );
        startOverlayOpen = false;
    }

    public void Update()
    {
        if( startOverlayOpen && Input.GetKeyDown( KeyCode.Space ) ) CloseStartOverlay();
    }
}
