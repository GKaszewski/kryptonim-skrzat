using UnityEngine;

public class Teleporter : MonoBehaviour {
    [SerializeField] private Transform target;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.transform.position = target.position;
        }
    }
}