using System.Runtime.Serialization;

[System.Serializable]
public class GameData : ISerializable {
    public int score;
    public int highScore;
    public int[] levelsCompleted;
    public int currentLevel;
    
    public GameData() { }
    
    public GameData(SerializationInfo info, StreamingContext context) {
        score = (int)info.GetValue("score", typeof(int));
        highScore = (int)info.GetValue("highScore", typeof(int));
        levelsCompleted = (int[])info.GetValue("levelsCompleted", typeof(int[]));
        currentLevel = (int)info.GetValue("currentLevel", typeof(int));
    }
    
    public void GetObjectData(SerializationInfo info, StreamingContext context) {
        info.AddValue("score", score, typeof(int));
        info.AddValue("highScore", highScore, typeof(int));
        info.AddValue("levelsCompleted", levelsCompleted, typeof(int[]));
        info.AddValue("currentLevel", currentLevel, typeof(int));
    }
}