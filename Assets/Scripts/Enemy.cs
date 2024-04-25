using UnityEngine;

public class Enemy : MonoBehaviour, IDamageInflector {
    [SerializeField] private float damage = 10f;
    public float Damage => damage;
}