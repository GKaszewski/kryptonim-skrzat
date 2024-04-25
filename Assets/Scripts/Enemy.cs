using UnityEngine;

public class Enemy : MonoBehaviour, IDamageInflector {
    [field: SerializeField] public float Damage { get; private set; } = 10f;
}