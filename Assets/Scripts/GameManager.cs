using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public CharacterAttributes player;
    public TimeManager timeManager;
    public UIController uiController;
    public ObjectiveManager objectiveManager;

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
        SaveGame();
    }

    private void OnDestroy() {
        SaveGame();
    }

    public void SaveGame() {
        var saveData = new GameData();
        saveData.score = player.Score;
        saveData.highScore = player.HighScore;
        saveData.currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        
        SaveSystem.SaveData(saveData);
    }
    
    public void LoadGame() {
        var saveData = SaveSystem.LoadData();
        // do something with the save data
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