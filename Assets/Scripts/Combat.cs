using System;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour {
    private Controls controls;
    private IWeapon currentWeapon;

    [SerializeField, Self] private Inventory inventory;
    [SerializeField] private WeaponData defaultWeaponData;
    
    private void OnEnable() {
        controls.Enable();
    }
    
    private void OnDisable() {
        controls.Disable();
    }

    private void Awake() {
        controls = new Controls();
        controls.Enable();
    }

    private void Start() {
        if (inventory.GetWeapons().Count == 0) {
            inventory.AddWeapon(defaultWeaponData);
        }
        SwitchWeapon(inventory.GetWeapons()[0]);
    }

    private void Update() {
        if (controls.Player.Attack.IsPressed() && currentWeapon != null) {
            currentWeapon.Attack();
        }
    }

    public void OnCycleWeapon(InputAction.CallbackContext context) {
        if (context.action.triggered) {
            CycleWeapons();
        }
    }

    private void SwitchWeapon(IWeapon weapon) {
        currentWeapon = weapon;
        currentWeapon.Initialize(transform);
    }

    private void CycleWeapons() {
        var weapons = inventory.GetWeapons();
        if (weapons.Count == 0) return;
        var currentIndex = weapons.IndexOf(currentWeapon);
        var nextIndex = (currentIndex + 1) % weapons.Count;
        SwitchWeapon(weapons[nextIndex]);
    }
}