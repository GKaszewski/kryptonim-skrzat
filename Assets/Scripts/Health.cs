using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private CharacterAttributes character;
    [SerializeField] private float maxHealth = 100f;

    private void Start() {
        if (!character) character = GetComponent<CharacterAttributes>();
        character.health = maxHealth;
    }
    
    public void TakeDamage(float damage) {
        character.ModifyHealth(-damage);
    }
}
