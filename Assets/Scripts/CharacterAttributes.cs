using System;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {
    public int score;
    public int highScore;
    public float health;
    
    public event Action<float> OnHealthChanged;
    public event Action OnDamage;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;
    
    public void ModifyHealth(float amount) {
        var newHealth = health + amount;
        if (Math.Abs(newHealth - health) < 0.01) return;

        health = newHealth;
        OnHealthChanged?.Invoke(health);
        if (amount < 0) OnDamage?.Invoke();
    }

    public void AddScore(int amount) {
        if (amount == 0) return;
        
        score += amount;
        OnScoreChanged?.Invoke(score);
        CheckForNewHighScore();
    }

    private void CheckForNewHighScore() {
        if (score <= highScore) return;
        
        highScore = score;
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void ResetScore() {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
}