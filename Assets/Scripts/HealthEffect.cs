using UnityEngine;

public class HealthEffect : MonoBehaviour, IItemEffect {
    [SerializeField] private float healthAmount = 10f;
    public void ApplyEffect(CharacterAttributes character) {
        character.ModifyHealth(healthAmount);
        DestroyItem();
    }
    
    public void DestroyItem() {
        Destroy(gameObject);
    }
}