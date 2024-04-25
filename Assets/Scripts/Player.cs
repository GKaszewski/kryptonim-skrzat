using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private CharacterAttributes character;
    
    private void Start() {
        if (!character) character = GetComponent<CharacterAttributes>();
        
        GameManager.Instance.player = character;
    }
}