using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingMovement : MonoBehaviour {
    private float currentSpeed;
    private Collider2D target;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectedSpeed = 10f;
    [SerializeField] private float directionChangeInterval = 3f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 direction = Vector2.right;

    private void Awake() {
        rb ??= GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(ChangeDirection());
    }

    private void Update() {
        CheckForTarget();
        SetSpeed();
    }

    private void FixedUpdate() {
        rb.velocity = direction * currentSpeed;
    }

    private IEnumerator ChangeDirection() {
        while (true) {
            yield return new WaitForSeconds(directionChangeInterval);

            if (target) continue;
            var angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
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