using KBCore.Refs;
using UnityEngine;

public class TrailEnabler : MonoBehaviour {
    [SerializeField, Child] private TrailRenderer trailRenderer;

    private void OnDisable() {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
    }

    private void OnEnable() {
        trailRenderer.emitting = true;
    }
}