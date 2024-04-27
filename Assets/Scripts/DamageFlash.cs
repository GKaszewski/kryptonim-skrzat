using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour {
    private static readonly int FlashColor = Shader.PropertyToID("_FlashTint");
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");

    [SerializeField] [ColorUsage(true, true)] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private SpriteRenderer[] renderers;
    [SerializeField] private Material[] materials;
    [SerializeField] private CharacterAttributes character;

    private void Awake() {
        if (!character) character = GetComponent<CharacterAttributes>();
        if (!character) {
            Debug.LogError("CharacterAttributes component not found.");
            return;
        }
        
        if (renderers.Length == 0) {
            renderers = GetComponentsInChildren<SpriteRenderer>();
        }
        if (renderers.Length == 0) {
            renderers = GetComponents<SpriteRenderer>();
        }
        
        Initialize();
        
        character.OnDamage += CallFlashEffect;
    }

    private void OnEnable() {
        character.OnDamage += CallFlashEffect;
    }
    
    private void OnDisable() {
        StopAllCoroutines();
        character.OnDamage -= CallFlashEffect;
    }

    private void Initialize() {
        materials = new Material[renderers.Length];
        for (var i = 0; i < renderers.Length; i++) {
            materials[i] = renderers[i].material;
            materials[i].SetFloat(FlashAmount, 0f);
        }
    }
    
    private void SetFlashColor() {
        foreach (var material in materials) {
            material.SetColor(FlashColor, flashColor);
        }
    }

    private void SetFlashAmount(float amount) {
        foreach (var material in materials) {
            material.SetFloat(FlashAmount, amount);
        }
    }
    
    private void CallFlashEffect() {
        StartCoroutine(FlashEffect());
    }
    
    private IEnumerator FlashEffect() {
        SetFlashColor();
        var elapsedTime = 0f;
        while (elapsedTime < flashDuration) {
            elapsedTime += Time.deltaTime;
            var currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }
}