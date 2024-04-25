using UnityEngine;

public class ScoreEffect : MonoBehaviour, IItemEffect {
    [SerializeField] private int scoreAmount = 10;
    public void ApplyEffect(CharacterAttributes character) {
        character.AddScore(scoreAmount);
        DestroyItem();
    }

    public void DestroyItem() {
        Destroy(gameObject);
    }
}