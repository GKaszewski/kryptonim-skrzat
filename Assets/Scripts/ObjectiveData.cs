using UnityEngine;

public abstract class ObjectiveData : ScriptableObject, IObjective {
    public string objectiveDescription;
    public string Description => objectiveDescription;
    public abstract bool IsCompleted(Inventory inventory);
}