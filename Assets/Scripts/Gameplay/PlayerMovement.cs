
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 30f;
    public float movementSmoothing = 0.12f;
    public Rigidbody2D rigidbody2d;
    public bool isMoving = false;

    private Vector2 cachedMovement;
    private Vector2 movementVelocity;

    private void Update()
    {
        Vector2 input = Player.alive ? new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) ) : Vector2.zero;
        cachedMovement = Vector2.SmoothDamp( cachedMovement, input, ref movementVelocity, movementSmoothing );

        isMoving = input != Vector2.zero;
    }

    private void FixedUpdate()
    {
        float speedModified = movementSpeed;
        if( PowerupManager.activePowerup == PowerupManager.ActivePowerup.Speed ) speedModified = speedModified * 1.4f;
        rigidbody2d.MovePosition( rigidbody2d.position + ( cachedMovement * speedModified * Time.fixedDeltaTime ) );
    }
}