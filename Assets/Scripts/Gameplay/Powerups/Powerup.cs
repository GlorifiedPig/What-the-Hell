
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public AudioClip pickupSound;
    public SpriteRenderer spriteRenderer;
    public float pickupDistance = 3f;
    public float fadeDivisor = 4f;
    public float fadeBeginDelay = 4f;

    public Player player;
    public PowerupManager powerupManager;
    public float fadingProgress = 1f;
    public bool fading = false;

    public void Start()
    {
        player = Player.Instance;
        powerupManager = PowerupManager.Instance;
        Invoke( nameof( BeginFading ), fadeBeginDelay );
    }

    public virtual void Pickup() {
        if( pickupSound ) powerupManager.audioSource.PlayOneShot( pickupSound );

        Destroy( gameObject );
    }

    public void Update()
    {
        if( Player.alive && Vector2.Distance( transform.position, player.transform.position ) <= pickupDistance )
            Pickup();

        if( fading )
        {
            fadingProgress -= Time.deltaTime / fadeDivisor;
            if( fadingProgress <= 0 ) { Destroy( gameObject ); return; }
            Color newColor = spriteRenderer.color;
            newColor.a = fadingProgress;
            spriteRenderer.color = newColor;
        }
    }

    public void BeginFading() => fading = true;
}
