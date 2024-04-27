using System;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {
    [SerializeField] private int _score;
    [SerializeField] private int _highScore;
    [SerializeField] private float _health;
    
    public event Action<float> OnHealthChanged;
    public event Action OnDamage;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;

    public int Score {
        get => _score;
        private set {
            if (_score == value) return;
            _score = value;
            OnScoreChanged?.Invoke(_score);
            CheckForNewHighScore();
        }
    }

    public int HighScore {
        get => _highScore;
        private set {
            if (_highScore == value) return;
            _highScore = value;
            OnHighScoreChanged?.Invoke(_highScore);
        }
    }

    public float Health {
        get => _health;
        private set {
            if (Math.Abs(_health - value) < 0.01) return;
            _health = value;
            OnHealthChanged?.Invoke(_health);
        }
    }

    public void ModifyHealth(float amount) {
        Health += amount;
        if (amount < 0) {
            OnDamage?.Invoke();
        }
    }
    
    public void SetHealth(float amount) {
        Health = amount;
    }
    
    public void SetScore(int amount) {
        Score = amount;
    }
    
    public void SetHighScore(int amount) {
        HighScore = amount;
    }

    public void AddScore(int amount) {
        if (amount == 0) return;
        Score += amount;
    }

    private void CheckForNewHighScore() {
        if (_score > HighScore) {
            HighScore = _score;
        }
    }

    public void ResetScore() {
        Score = 0;
    }
}