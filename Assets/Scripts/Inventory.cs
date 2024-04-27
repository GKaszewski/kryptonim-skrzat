using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private Dictionary<string, Item> items = new Dictionary<string, Item>();
    private List<IWeapon> weapons = new List<IWeapon>();
    
    public event Action<Item> OnInventoryChanged;

    [SerializeField] private List<WeaponData> weaponDatas;
    [SerializeField] private Transform defaultSpawnPoint;

    private void Awake() {
        foreach (var weaponData in weaponDatas) {
            AddWeapon(weaponData);
        }
    }

    public void AddItem(Item item) {
        if (items.TryGetValue(item.itemName, out var existingItem)) {
            existingItem.count += item.count;
            OnInventoryChanged?.Invoke(existingItem);
        } else {
            items.Add(item.itemName, item);
            OnInventoryChanged?.Invoke(item);
        }
    }
    
    public bool RemoveItem(Item item) {
        if (items.TryGetValue(item.itemName, out var existingItem)) {
            if (existingItem.count > item.count) {
                existingItem.count -= item.count;
                OnInventoryChanged?.Invoke(existingItem);
                return true;
            }
            
            items.Remove(item.itemName);
            OnInventoryChanged?.Invoke(item);
            return true;
        }

        return false;
    }
    
    public bool ContainsItem(Item item) {
        return items.ContainsKey(item.itemName);
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

    public int GetItemCount(string itemName) {
        return items.TryGetValue(itemName, out var item) ? item.count : 0;
    }
}