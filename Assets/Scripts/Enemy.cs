using UnityEngine;

public class Enemy : MonoBehaviour, IDamageInflector {
    public float Damage { get; private set; } = 10f;
}