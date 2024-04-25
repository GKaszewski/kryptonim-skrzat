using KBCore.Refs;
using UnityEngine;

public class EdgeBouncer : MonoBehaviour {
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rayLength = 1.0f;
    [SerializeField, Self] private Rigidbody2D rb;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    
    private void Update() {
        SetDirection();
    }
    
    private void FixedUpdate() {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckLeft.position, groundCheckLeft.position + Vector3.down * rayLength);
        Gizmos.DrawLine(groundCheckRight.position, groundCheckRight.position + Vector3.down * rayLength);
    }
    
    private bool IsGroundedLeft() {
        return Physics2D.Raycast(groundCheckLeft.position, Vector2.down, rayLength, walkableLayer);
    }
    
    private bool IsGroundedRight() {
        return Physics2D.Raycast(groundCheckRight.position, Vector2.down, rayLength, walkableLayer);
    }

    private void SetDirection() {
        var left = IsGroundedLeft();
        var right = IsGroundedRight();

        if (right && !left) {
            direction = Vector2.right;
        } else if (!right && left) {
            direction = Vector2.left;
        }
        
        if (!right && !left) {
            direction = Vector2.zero;
        }
    }
}