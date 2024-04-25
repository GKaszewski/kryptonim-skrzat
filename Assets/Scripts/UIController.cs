using KBCore.Refs;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {
    private ProgressBar healthBar;
    private Label keysLabel;
    private Label scoreLabel;
    private Label highScoreLabel;

    [SerializeField, Self] private UIDocument document;
    [SerializeField] private CharacterAttributes player;

    private void Awake() {
        var root = document.rootVisualElement;
        healthBar = root.Q<ProgressBar>("health-bar");
        keysLabel = root.Q<Label>("keys-label");
        scoreLabel = root.Q<Label>("score-label");
        highScoreLabel = root.Q<Label>("high-score-label");

        healthBar.value = player.health;
        UpdateKeysLabel(0);
        UpdateScoreLabel(player.score);
        UpdateHighScoreLabel(player.highScore);

        if (!player) return;
        player.OnHealthChanged += UpdateHealthBar;
        player.OnScoreChanged += UpdateScoreLabel;
        player.OnHighScoreChanged += UpdateHighScoreLabel;
    }

    private void OnEnable() {
        if (!player) return;
        
        player.OnHealthChanged += UpdateHealthBar;
        player.OnScoreChanged += UpdateScoreLabel;
        player.OnHighScoreChanged += UpdateHighScoreLabel;
    }

    private void OnDisable() {
        player.OnHealthChanged -= UpdateHealthBar;
        player.OnScoreChanged -= UpdateScoreLabel;
        player.OnHighScoreChanged -= UpdateHighScoreLabel;
    }

    private void UpdateHealthBar(float value) {
        if (healthBar == null) return;
        healthBar.value = value;
    }
    
    public void UpdateKeysLabel(int value) {
        if (keysLabel == null) return;
        keysLabel.text = $"Keys: {value}";
    }

    private void UpdateScoreLabel(int value) {
        if (scoreLabel == null) return;
        scoreLabel.text = $"Score: {value}";
    }

    private void UpdateHighScoreLabel(int value) {
        if (highScoreLabel == null) return;
        highScoreLabel.text = $"High Score: {value}";
    }
}