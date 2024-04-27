using KBCore.Refs;
using UnityEngine;

public class Collector : MonoBehaviour {
    [SerializeField, Self] private CharacterAttributes player;
    [SerializeField, Self] private Inventory inventory;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Item")) return;
        var itemEffects = other.GetComponents<IItemEffect>();
        var item = other.GetComponent<DroppedItem>();
        foreach (var effect in itemEffects) {
            effect.ApplyEffect(player);
        }

        item?.OnPickup(inventory);
    }
}