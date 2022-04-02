
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 25f;
    public Rigidbody2D rigidbody2d;

    private Vector2 cachedMovement;

    private void Update()
    {
        cachedMovement.x = Input.GetAxisRaw( "Horizontal" );
        cachedMovement.y = Input.GetAxisRaw( "Vertical" );
    }

    private void FixedUpdate()
    {
        rigidbody2d.MovePosition( rigidbody2d.position + ( cachedMovement * movementSpeed * Time.fixedDeltaTime ) );
    }
}