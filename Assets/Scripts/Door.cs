using KBCore.Refs;
using UnityEngine;

public class Door : MonoBehaviour {
    private bool isOpen;
    
    [SerializeField, Self] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;

    private void OnEnable() {
        if (!GameManager.Instance) {
            return;
        }
        GameManager.Instance.objectiveManager.OnObjectiveCompleted += OnObjectiveCompleted;
    }

    private void Start() {
        GameManager.Instance.objectiveManager.OnObjectiveCompleted += OnObjectiveCompleted;
    }

    private void OnDisable() {
        GameManager.Instance.objectiveManager.OnObjectiveCompleted -= OnObjectiveCompleted;
    }

    private void OnObjectiveCompleted() {
        spriteRenderer.sprite = openSprite;
        isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isOpen && other.CompareTag("Player")) {
            // Complete the level
        }
    }
}