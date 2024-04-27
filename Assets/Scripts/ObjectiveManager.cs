using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {
    private List<IObjective> objectives = new List<IObjective>();
    [SerializeField] private List<ObjectiveData> data = new List<ObjectiveData>();

    public event Action OnObjectiveCompleted;

    private void Awake() {
        InitializeObjectivesFromData();
    }

    public void InitializeObjectivesFromData() {
        foreach (var objectiveData in data) {
            AddObjective(objectiveData);
        }
    }

    public void AddObjective(IObjective objective) {
        objectives.Add(objective);
    }

    public void UpdateObjectives(Inventory inventory) {
        foreach (var objective in objectives) {
            if (objective.IsCompleted(inventory)) {
                OnObjectiveCompleted?.Invoke();
            }
        }
    }
}