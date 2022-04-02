
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 30f;
    public float movementSmoothing = 0.12f;
    public Rigidbody2D rigidbody2d;

    private Vector2 cachedMovement;
    private Vector2 movementVelocity;

    private void Update()
    {
        Vector2 input = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) );
        cachedMovement = Vector2.SmoothDamp( cachedMovement, input, ref movementVelocity, movementSmoothing );
    }

    private void FixedUpdate()
    {
        rigidbody2d.MovePosition( rigidbody2d.position + ( cachedMovement * movementSpeed * Time.fixedDeltaTime ) );
    }
}