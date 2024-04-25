using UnityEngine;

public class Collector : MonoBehaviour {
    [SerializeField]
    private CharacterAttributes player;
    [SerializeField] private Inventory inventory;

    private void Awake() {
        if (!player) player = GetComponent<CharacterAttributes>();
        if (!inventory) inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Item")) return;
        var itemEffects = other.GetComponents<IItemEffect>();
        var item = other.GetComponent<IItem>();
        inventory.AddItem(item);
        foreach (var effect in itemEffects) {
            effect.ApplyEffect(player);
        }

        item?.OnPickup(inventory);
    }
}