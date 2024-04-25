using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private CharacterAttributes character;
    [SerializeField] private float maxHealth = 100f;

    private void Start() {
        character ??= GetComponent<CharacterAttributes>();
        if (!character) {
            Debug.LogError("CharacterAttributes component not found.");
            return;
        }
        character.health = maxHealth;
    }
    
    public void TakeDamage(float damage) {
        character.ModifyHealth(-damage);
    }
}
