using KBCore.Refs;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField, Self] private CharacterAttributes character;
    [SerializeField] private float maxHealth = 100f;

    private void Start() {
        character.SetHealth(maxHealth);
    }
    
    public void TakeDamage(float damage) {
        character.ModifyHealth(-damage);
    }
}
