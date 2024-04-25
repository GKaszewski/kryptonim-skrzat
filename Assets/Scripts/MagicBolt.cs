using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagicBolt : MonoBehaviour {
    private Rigidbody2D rb;
    
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        StartCoroutine(DisableAfterTime());
    }

    public void Launch(Vector2 direction) {
        rb.velocity = direction * speed;
    }
    
    private IEnumerator DisableAfterTime() {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        gameObject.SetActive(false);
        
        var health = other.GetComponent<Health>();
        if (health) health.TakeDamage(damage);
    }
}