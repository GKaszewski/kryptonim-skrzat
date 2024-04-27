using System;

[Serializable]
public class Item : IPickable {
    public string itemName;
    public int count;
    
    public void OnPickup(Inventory inventory) {
        inventory.AddItem(this);
        GameManager.Instance.objectiveManager.UpdateObjectives(inventory);
    }
}