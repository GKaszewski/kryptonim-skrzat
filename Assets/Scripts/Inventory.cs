using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private List<IItem> items = new List<IItem>();
    private List<IWeapon> weapons = new List<IWeapon>();

    [SerializeField] private List<WeaponData> weaponDatas;
    [SerializeField] private Transform defaultSpawnPoint;

    private void Awake() {
        foreach (var weaponData in weaponDatas) {
            AddWeapon(weaponData);
        }
    }

    public void AddItem(IItem item) {
        items.Add(item);
    }
    
    public bool RemoveItem(IItem item) {
        return items.Remove(item);
    }
    
    public bool ContainsItem(IItem item) {
        return items.Contains(item);
    }
    
    public List<IWeapon> GetWeapons() {
        return weapons;
    }

    public void AddWeapon(WeaponData data) {
        var weaponGameObject = Instantiate(data.weaponPrefab, defaultSpawnPoint.position, Quaternion.identity);
        weaponGameObject.transform.SetParent(transform);
        var weapon = weaponGameObject.GetComponent<IWeapon>();
        if (weapon != null) {
            weapon.Initialize(defaultSpawnPoint);
            weapons.Add(weapon);
        }
    }

    public List<IItem> GetKeys() {
        return items.FindAll(item => item is Key);
    }
}