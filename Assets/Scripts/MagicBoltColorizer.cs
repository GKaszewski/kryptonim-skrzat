using KBCore.Refs;
using UnityEngine;

public class MagicBoltColorizer : MonoBehaviour {
    private static readonly int Tint = Shader.PropertyToID("_Tint");
    
    [SerializeField, Child] private Renderer[] objectRenderers;
    [SerializeField, Child] private TrailRenderer trailRenderer;
    [SerializeField] private Material[] materials;
    [SerializeField] [ColorUsage(true, true)] private Color color = Color.cyan;
    [SerializeField] private float opacityAtEnd = 0.1f;

    private void Awake() {
        Initialize();
        SetColor();
    }

    private void Initialize() {
        materials = new Material[objectRenderers.Length];
        for (var i = 0; i < objectRenderers.Length; i++) {
            materials[i] = objectRenderers[i].material;
        }
    }
    
    private void SetColor() {
        foreach (var material in materials) {
            material.SetColor(Tint, color);
        }

        if (!trailRenderer) return;
        trailRenderer.startColor = color;
        trailRenderer.endColor = new Color(color.r, color.g, color.b, opacityAtEnd);
    }
    
}