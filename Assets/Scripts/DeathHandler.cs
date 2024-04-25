using KBCore.Refs;
using UnityEngine;

public class DeathHandler : MonoBehaviour {
    private IDeathBehavior deathBehavior;
    
    [SerializeField, Self] private CharacterAttributes character;

    private void Awake() {
        if (deathBehavior == null) deathBehavior = GetComponent<IDeathBehavior>();

        character.OnHealthChanged += OnHealthChanged;
    }

    private void OnEnable() {
        if (character) {
            character.OnHealthChanged += OnHealthChanged;
        }
    }

    private void OnDisable() {
        character.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float _) {
        if (character.health <= 0f) {
            Die();
        }
    }

    private void Die() {
        if (deathBehavior == null) {
            Debug.LogError("Death behavior not found.");
            Destroy(gameObject);
            return;
        }
        
        deathBehavior.Die();
    }
}