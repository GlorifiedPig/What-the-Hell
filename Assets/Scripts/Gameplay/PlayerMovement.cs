
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepSounds;

    public float movementSpeed = 30f;
    public float movementSmoothing = 0.12f;
    public Rigidbody2D rigidbody2d;
    public bool isMoving = false;

    private float nextFootstep = 0f;
    private Vector2 cachedMovement;
    private Vector2 movementVelocity;

    private void Update()
    {
        Vector2 input = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) );
        cachedMovement = Vector2.SmoothDamp( cachedMovement, input, ref movementVelocity, movementSmoothing );

        isMoving = input != Vector2.zero;

        if( isMoving && Time.time >= nextFootstep )
        {
            footstepAudioSource.PlayOneShot( footstepSounds[Random.Range( 0, footstepSounds.Length - 1 )] );
            nextFootstep = Time.time + Random.Range( 0.35f, 0.5f );
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.MovePosition( rigidbody2d.position + ( cachedMovement * movementSpeed * Time.fixedDeltaTime ) );
    }
}