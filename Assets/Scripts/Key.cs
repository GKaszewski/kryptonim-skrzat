using UnityEngine;

public class Key : MonoBehaviour, IItem {
    [SerializeField] private string itemName = "Key";
    public string Name => itemName;

    public void OnPickup(Inventory inventory) {
        var keys = inventory.GetKeys().Count;
        GameManager.Instance.uiController.UpdateKeysLabel(keys);
        Destroy(gameObject);
    }
}