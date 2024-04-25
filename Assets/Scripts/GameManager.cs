using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public CharacterAttributes player;
    public TimeManager timeManager;
    public UIController uiController;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (!timeManager) timeManager = GetComponent<TimeManager>();
    }

    public void RestartGame() {
        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void IncreaseScore(int scoreAmount) {
        player.AddScore(scoreAmount);
    }

    public void ResetPlayerScore() {
        player.ResetScore();
    }
}