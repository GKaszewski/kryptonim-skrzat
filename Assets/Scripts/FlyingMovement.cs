using System;
using KBCore.Refs;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingMovement : MonoBehaviour {
    private float directionTimer = 0f;
    private float currentSpeed;
    private Collider2D target;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectedSpeed = 10f;
    [SerializeField] private float directionChangeInterval = 3f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField, Self] private Rigidbody2D rb;
    [SerializeField, Self] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float rotationSpeed = 5f;
    
    private void Start() {
        ChangeDirection();
    }

    private void Update() {
        CheckForTarget();
        SetSpeed();
        HandleChangeDirection();
    }

    private void HandleChangeDirection() {
        directionTimer += Time.deltaTime;
        if (!(directionTimer >= directionChangeInterval)) return;
        
        directionTimer = 0f;
        ChangeDirection();
    }

    private void FixedUpdate() {
        rb.velocity = direction * currentSpeed;
    }
    
    private void RotateTowardsTarget() {
        if (!target) return;
        
        var targetDirection = (target.transform.position - transform.position).normalized;
        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
        
        spriteRenderer.flipY = angle < 0;
    }
    
    private void FlipSprite() {
        if (target) return;
        
        if (direction.x > 0) {
            spriteRenderer.flipX = false;
        } else if (direction.x < 0) {
            spriteRenderer.flipX = true;
        }
    }

    private void LateUpdate() {
        RotateTowardsTarget();
    }

    private void ChangeDirection() {
        if (target) return;
        var angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
    
    private void CheckForTarget() {
        target = Physics2D.OverlapCircle(transform.position, detectionRadius, targetLayer);
        if (!target) return;
        
        direction = (target.transform.position - transform.position).normalized;
    }

    private void SetSpeed() {
        currentSpeed = target ? detectedSpeed : speed;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}