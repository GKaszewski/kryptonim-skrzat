using UnityEngine;

namespace DefaultNamespace {
    [CreateAssetMenu(fileName = "New CollectItemsObjective", menuName = "Objectives/Collect items objective", order = 0)]
    public class CollectItemsObjectiveData : ObjectiveData {
        public ItemRequirement[] requirements;
        
        public override bool IsCompleted(Inventory inventory) {
            foreach (var requirement in requirements) {
               if (inventory.GetItemCount(requirement.itemName) < requirement.requiredAmount) {
                   return false;
               }
            }
            return true;
        }
        
        
        [System.Serializable]
        public struct ItemRequirement {
            public string itemName;
            public int requiredAmount;
        }
    }
}