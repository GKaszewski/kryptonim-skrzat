using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Inventory/Weapon", order = 1)]
public class WeaponData : ScriptableObject {
    public string weaponName;
    public GameObject weaponPrefab;
    public float damage;
    public float attackRate;
    public AudioClip attackSound;
    public int prefabIndex;
}