using UnityEngine;

public class DroppedItem : MonoBehaviour, IPickable {
    [SerializeField] private Item item;
    [SerializeField] private ItemData itemData;
    
    private void Awake() {
        CreateItemFromData();
    }
    
    private void CreateItemFromData() {
        item = new Item {
            itemName = itemData.itemName,
            count = itemData.count
        };
    }
    
    public void OnPickup(Inventory inventory) {
        item.OnPickup(inventory);
        Destroy(gameObject);
    }
}