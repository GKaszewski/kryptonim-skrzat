using KBCore.Refs;
using UnityEngine;

public class EdgeBouncer : MonoBehaviour {
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rayLength = 1.0f;
    [SerializeField, Self] private Rigidbody2D rb;
    [SerializeField, Self] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private float leftSpaceRayLength = 0.2f;
    [SerializeField] private float rightSpaceRayLength = 0.1f;
    
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * rayLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * rayLength);
    }
    
    private bool IsGroundedLeft() {
        return Physics2D.Raycast(groundCheckLeft.position, Vector2.down, rayLength, walkableLayer);
    }
    
    private bool IsGroundedRight() {
        return Physics2D.Raycast(groundCheckRight.position, Vector2.down, rayLength, walkableLayer);
    }
    
    private bool IsGroundedMiddle() {
        return Physics2D.Raycast(transform.position, Vector2.down, rayLength, walkableLayer);
    }
    
    private bool HasSpaceLeft() {
        return !Physics2D.Raycast(transform.position, Vector2.left, leftSpaceRayLength, walkableLayer);
    }
    
    private bool HasSpaceRight() {
        return !Physics2D.Raycast(transform.position, Vector2.right, rightSpaceRayLength, walkableLayer);
    }
    
    private void SetDirection() {
        var left = IsGroundedLeft();
        var right = IsGroundedRight();
        var groundedMiddle = IsGroundedMiddle();
        var spaceLeft = HasSpaceLeft();
        var spaceRight = HasSpaceRight();
        
        if (right && !left) {
            direction = Vector2.right;
            spriteRenderer.flipX = false;
        } else if (!right && left) {
            direction = Vector2.left;
            spriteRenderer.flipX = true;
        }
        
        if (spaceLeft && !spaceRight) {
            direction = Vector2.left;
            spriteRenderer.flipX = true;
        } else if (!spaceLeft && spaceRight) {
            direction = Vector2.right;
            spriteRenderer.flipX = false;
        }
        
        // if on both sides there is space, choose the one with the most space
        if (groundedMiddle) {
            if (spaceLeft && spaceRight) {
                if (Physics2D.Raycast(transform.position, Vector2.left, rayLength * leftSpaceRayLength, walkableLayer)) {
                    direction = Vector2.right;
                    spriteRenderer.flipX = false;
                } else if (Physics2D.Raycast(transform.position, Vector2.right, rayLength * rightSpaceRayLength, walkableLayer)) {
                    direction = Vector2.left;
                    spriteRenderer.flipX = true;
                }
            }
        }
        
        // no ground, no space
        if (!right && !left && !spaceLeft && !spaceRight) {
            direction = Vector2.zero;
        }
    }
}