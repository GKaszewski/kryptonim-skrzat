using UnityEngine;

[CreateAssetMenu(fileName = "New CollectScoreObjective", menuName = "Objectives/Collect score objective")]
public class CollectScoreObjectiveData : ObjectiveData {
    public int requiredScore;
    
    public override bool IsCompleted(Inventory inventory) {
        return GameManager.Instance.player.Score >= requiredScore;
    }
}