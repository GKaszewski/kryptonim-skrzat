using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour, Controls.IPlayerActions {
    private Controls controls;
    private Rigidbody2D rb;
    private float horizontal;
    private bool jumped;
    private bool jumpedUp;
    [HideInInspector] public bool isFacingRight;
    private bool isJumping;
    private float lastGroundTime;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float speed;
    [SerializeField] private float friction = 0.2f;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float jumpPower;
    [SerializeField] private float velocityPower = 0.9f;
    [SerializeField] private float fallGravityMultiplier = 0.9f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float gravityScale = 4f;
    [SerializeField] private float groundDetectionRadius = 0.1f;
    [SerializeField] private Transform graphics;
    [SerializeField] private float maxDownwardVelocity = 10f;
    [SerializeField] private bool applyJumpCut = true;
    [SerializeField] private bool applyFallGravity = true;
    [SerializeField] private bool capVelocity = true;

    private void Awake() {
        controls = new Controls();
        controls.Player.AddCallbacks(this);
    }

    private void OnEnable() {
        controls.Enable();
    }
    
    private void OnDisable() {
        controls.Disable();
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (IsGrounded()) {
            lastGroundTime += Time.deltaTime;
            coyoteTimeCounter = coyoteTime;
        } else {
            lastGroundTime = 0f;
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumped) {
            jumpBufferCounter = jumpBufferTime;
        }
        else {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        Flip();
    }

    private void FixedUpdate() {
        Move();
        ApplyFriction();
        ApplyJump();
        if (applyJumpCut) ApplyJumpCut();
        if (applyFallGravity) ApplyFallGravity();
        if (capVelocity) CapVelocity();
    }

    public void OnMovement(InputAction.CallbackContext context) {
        horizontal = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        jumped = context.action.IsPressed();
        jumpedUp = context.action.WasReleasedThisFrame();
    }

    public void OnAttack(InputAction.CallbackContext context) {
    }

    public void OnCycleWeapon(InputAction.CallbackContext context) {
        
    }

    private void ApplyJump() {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping) {
            // play audio
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpBufferCounter = 0f;
            StartCoroutine(JumpCooldown());
        }
    }

    private void ApplyJumpCut() {
        if (!jumpedUp) return;
        if (rb.velocity.y <= 0f || !isJumping) return;
        rb.AddForce(Vector2.down * (rb.velocity.y * (1f - jumpCutMultiplier)), ForceMode2D.Impulse);
        jumpBufferCounter = 0f;
    }

    private void ApplyFallGravity() {
        if (rb.velocity.y < 0f) {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
            return;
        }
        
        rb.gravityScale = gravityScale;
    }
    
    private void ApplyFriction() {
        if (lastGroundTime <= 0f || Mathf.Abs(horizontal) >= 0.01f) return;
        var num = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));
        num *= Mathf.Sign(rb.velocity.x);
        rb.AddForce(num * Vector2.left, ForceMode2D.Impulse);
    }

    private void Move() {
        var num = horizontal * speed;
        var f = num - rb.velocity.x;
        var num2 = (Mathf.Abs(num) > 0.01f) ? acceleration : deceleration;
        var d = Mathf.Pow(Mathf.Abs(f) * num2, velocityPower) * Mathf.Sign(f);
        rb.AddForce(d * Vector2.right);
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, groundDetectionRadius, groundLayer);
    }

    private void Flip() {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f)) {
            isFacingRight = !isFacingRight;
        }

        if (horizontal > 0f) {
            transform.localEulerAngles = Vector3.zero;
            return;
        }

        if (horizontal < 0f) {
            transform.localEulerAngles = new Vector3(0, 180f, 0);
        }
    }

    private IEnumerator JumpCooldown() {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
    
    private void CapVelocity() {
        if (rb.velocity.y < -maxDownwardVelocity) {
            rb.velocity = new Vector2(rb.velocity.x, -maxDownwardVelocity);
        }
    }
}