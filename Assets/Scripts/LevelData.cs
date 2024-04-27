using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Levels/Level", order = 0)]
public class LevelData : ScriptableObject {
    public Level level;
}