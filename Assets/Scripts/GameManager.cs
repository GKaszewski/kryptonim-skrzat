using KBCore.Refs;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public CharacterAttributes player;
    public TimeManager timeManager;
    public UIController uiController;
    public ObjectiveManager objectiveManager;
    [SerializeField, Scene] public LevelsManager levelsManager;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        if (!timeManager) timeManager = GetComponent<TimeManager>();
        if (!objectiveManager) objectiveManager = GetComponent<ObjectiveManager>();
    }

    private void OnDisable() {
        //SaveGame();
    }

    public void SaveGame() {
        var saveData = new GameData();
        var unlockedLevels = LevelsManager.Instance.GetUnlockedLevels();
        var completedLevels = LevelsManager.Instance.GetCompletedLevels();
        
        saveData.score = player.Score;
        saveData.highScore = player.HighScore;
        saveData.currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        saveData.levelsUnlocked = unlockedLevels;
        saveData.levelsCompleted = completedLevels;
        SaveSystem.SaveData(saveData);
    }
    
    public void LoadGame() {
        var saveData = SaveSystem.LoadData();
        LevelsManager.Instance.UnlockLevels(saveData.levelsUnlocked);
        LevelsManager.Instance.CompleteLevels(saveData.levelsCompleted);
        LevelsManager.Instance.currentLevelIndex = saveData.currentLevel;
        player.SetScore(saveData.score);
        player.SetHighScore(saveData.highScore);
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