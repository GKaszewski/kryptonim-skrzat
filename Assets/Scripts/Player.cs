using KBCore.Refs;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField, Self] private CharacterAttributes character;
    
    private void Start() {
        GameManager.Instance.player = character;
    }
}