
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    public AudioClip[] damagedAudio;
    public AudioSource audioSource;
    public Image healthImage;
    public Image armorImage;
    public GameObject armorDisplay;
    public GameObject deathOverlay;
    public float maxHealth = 100f;
    public float health = 100f;
    public float armor = 0f;
    public float maxArmor = 100f;
    public static bool alive = true;

    public static Player Instance;
    public static event Action PlayerDeath;

    public void OnEnable() => Instance = this;

    public void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        healthImage.fillAmount = Mathf.Lerp( healthImage.fillAmount, health / maxHealth, Time.deltaTime * 5f );
        armorImage.fillAmount = Mathf.Lerp( armorImage.fillAmount, armor / maxArmor, Time.deltaTime * 5f );
        armorDisplay.SetActive( armor > 0 );
    }

    public void Die()
    {
        if( !alive ) return;
        alive = false;
        spriteRenderer.sprite = deadSprite;
        PlayerDeath.Invoke();
        deathOverlay.SetActive( true );
    }

    public void TakeDamage( float damage )
    {
        if( armor > 0 )
        {
            float newArmor = armor - damage;
            if( newArmor < 0 )
            {
                damage = Mathf.Abs( newArmor ); // The damage left over is whatever the armor goes into the negatives.
                newArmor = 0;
            } else damage = 0;
            armor = newArmor;
        }

        if( damage > 0 )
        {
            float newHealth = Mathf.Max( health - damage, 0 );
            if( newHealth <= 0 ) Die();
            health = newHealth;
        }

        audioSource.PlayOneShot( damagedAudio[Random.Range( 0, damagedAudio.Length )] );
    }

    public void AddHealth( float addedHealth ) => health += Mathf.Min( addedHealth, maxHealth );
    public void AddArmor( float addedArmor ) => armor += Mathf.Min( addedArmor, maxArmor );
}
