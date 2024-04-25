using KBCore.Refs;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {
    [SerializeField, Self] private CharacterAttributes character;
    [SerializeField, Self] private InvulnerabilityManager invulnerabilityManager;

    private void OnTriggerEnter2D(Collider2D other) {
        InflictDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other) {
        InflictDamage(other);
    }

    private void InflictDamage(Collider2D other) {
        if (!other.CompareTag("Enemy")) return;
        if (invulnerabilityManager.IsInvulnerable) return;

        var damageInflector = other.GetComponent<IDamageInflector>();
        if (damageInflector == null) return;
        character.ModifyHealth(-damageInflector.Damage);
    }
}