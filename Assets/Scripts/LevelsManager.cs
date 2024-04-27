using UnityEngine;

public class LevelsManager : MonoBehaviour {
    public static LevelsManager Instance { get; private set; }

    public LevelData[] levelsInitialData;
    public Level[] levels;
    
    public int currentLevelIndex;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        CreateLevelsFromData();
    }

    private void Start() {
        GameManager.Instance.objectiveManager.OnObjectiveCompleted += OnObjectiveCompleted;
    }

    private void OnDisable() {
        GameManager.Instance.objectiveManager.OnObjectiveCompleted -= OnObjectiveCompleted;
    }

    public void CreateLevelFromData(int index) {
        levels[index] = new Level() {
            levelName = levelsInitialData[index].level.levelName,
            levelIndex = levelsInitialData[index].level.levelIndex,
            isUnlocked = levelsInitialData[index].level.isUnlocked,
            isCompleted = levelsInitialData[index].level.isCompleted,
            objective = levelsInitialData[index].level.objective
        };
    }
    
    public void UnlockLevel(int index) {
        levels[index].isUnlocked = true;
    }
    
    public void UnlockLevels(int[] indexes) {
        for (int i = 0; i < indexes.Length; i++) {
            levels[indexes[i]].isUnlocked = true;
        }
    }
    
    public void CompleteLevel(int index) {
        levels[index].isCompleted = true;
    }
    
    public void CompleteLevels(int[] indexes) {
        for (int i = 0; i < indexes.Length; i++) {
            levels[indexes[i]].isCompleted = true;
        }
    }
    
    public void ResetLevel(int index) {
        levels[index].isCompleted = false;
    }
    
    public void ResetAllLevels() {
        for (int i = 0; i < levels.Length; i++) {
            levels[i].isCompleted = false;
        }
    }
    
    public void ResetAllLevelsUnlocked() {
        for (int i = 0; i < levels.Length; i++) {
            levels[i].isUnlocked = false;
        }
    }
    
    public void CreateLevelsFromData() {
        levels = new Level[levelsInitialData.Length];
        
        for (int i = 0; i < levels.Length; i++) {
            CreateLevelFromData(i);
        }
    }
    
    public int[] GetUnlockedLevels() {
        var unlockedLevels = new int[levels.Length];
        var index = 0;
        
        for (int i = 0; i < levels.Length; i++) {
            if (levels[i].isUnlocked) {
                unlockedLevels[index] = i;
                index++;
            }
        }
        
        return unlockedLevels;
    }
    
    public int[] GetCompletedLevels() {
        var completedLevels = new int[levels.Length];
        var index = 0;
        
        for (int i = 0; i < levels.Length; i++) {
            if (levels[i].isCompleted) {
                completedLevels[index] = i;
                index++;
            }
        }
        
        return completedLevels;
    }
    
    private void OnObjectiveCompleted() {
        CompleteLevel(currentLevelIndex);
        UnlockLevel(currentLevelIndex + 1);
    }
}