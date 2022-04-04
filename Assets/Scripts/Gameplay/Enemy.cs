using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    public GameObject healthDisplay;
    public Image healthImage;
    public float maxHealth = 100f;
    public float health = 100f;
    public bool alive = true;
    public AIPath aiPath;

    private float deathFading = 1f;

    private void OnEnable() => WeaponBase.BulletHit += BulletHit;
    private void OnDisable() => WeaponBase.BulletHit -= BulletHit;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthImage.fillAmount = Mathf.Lerp( healthImage.fillAmount, health / maxHealth, Time.deltaTime * 5f );
        if( aiPath.desiredVelocity.x >= 0.01f ) transform.localScale = new Vector3( -1f, 1f, 1f );
        else transform.localScale = new Vector3( 1f, 1f, 1f );

        if( !alive )
        {
            deathFading -= Time.deltaTime / 10f;
            if( deathFading <= 0 ) { Destroy( gameObject ); return; }
            Color newColor = spriteRenderer.color;
            newColor.a = deathFading;
            spriteRenderer.color = newColor;
        }
    }

    public void TakeDamage( float damage )
    {
        float newHealth = Mathf.Max( health - damage, 0 );
        if( newHealth <= 0 ) Die();
        health = newHealth;
    }

    public void Die()
    {
        if( !alive ) return;
        aiPath.canMove = false;
        alive = false;
        spriteRenderer.sprite = deadSprite;
        transform.rotation = Quaternion.Euler( transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f );
        healthDisplay.SetActive( false );
    }

    private void BulletHit( RaycastHit2D ray, float damage )
    {
        if( alive && ray.transform == this.transform )
            TakeDamage( damage );
    }
}
