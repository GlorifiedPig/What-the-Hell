using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour
{
    public string playerTag = "Player";
    public Collider2D enemyCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    public GameObject healthDisplay;
    public Transform canvas;
    public Image healthImage;
    public float maxHealth = 100f;
    public float health = 100f;
    public float decayDelay = 5f;
    public bool alive = true;
    public AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;

    private bool startedDecaying = false;
    private float deathFading = 1f;

    public static event Action<Enemy> EnemyDeath = ( enemy ) => { };

    private void OnEnable() => WeaponBase.BulletHit += BulletHit;
    private void OnDisable() => WeaponBase.BulletHit -= BulletHit;

    private void Start()
    {
        health = maxHealth;
        aiDestinationSetter.target = GameObject.FindGameObjectWithTag( playerTag ).transform;
    }

    private void Update()
    {
        healthImage.fillAmount = Mathf.Lerp( healthImage.fillAmount, health / maxHealth, Time.deltaTime * 5f );
        if( aiPath.desiredVelocity.x >= 0.01f )
        {
            transform.localScale = new Vector3( -1f, 1f, 1f );
            canvas.localScale = new Vector3( -1f, 1f, 1f );
        } else
        {
            transform.localScale = new Vector3( 1f, 1f, 1f );
            canvas.localScale = new Vector3( 1f, 1f, 1f );
        }

        if( !alive && startedDecaying )
        {
            deathFading -= Time.deltaTime / 5f;
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

    public void BeginDecay()
    {
        if( alive ) return;
        startedDecaying = true;
    }

    public void Die()
    {
        if( !alive ) return;
        aiPath.canMove = false;
        alive = false;
        spriteRenderer.sprite = deadSprite;
        transform.rotation = Quaternion.Euler( transform.rotation.x, transform.rotation.y, transform.rotation.z - 90f );
        healthDisplay.SetActive( false );
        enemyCollider.enabled = false;
        Invoke( nameof( BeginDecay ), decayDelay );

        Vector3 newPosition = transform.position;
        newPosition.z += 1; // Let's get out of the way of the other zombies.
        transform.position = newPosition;

        EnemyDeath.Invoke( this );
    }

    private void BulletHit( RaycastHit2D ray, float damage )
    {
        if( alive && ray.transform == this.transform )
            TakeDamage( damage );
    }
}
