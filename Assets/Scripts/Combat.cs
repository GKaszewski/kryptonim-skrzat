using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour, Controls.IPlayerActions {
    private Controls controls;
    
    [SerializeField] private IWeapon currentWeapon;
    [SerializeField] private Inventory inventory;
    [SerializeField] private WeaponData defaultWeaponData;
    
    private void OnEnable() {
        controls.Enable();
    }
    
    private void OnDisable() {
        controls.Disable();
    }

    private void Awake() {
        controls = new Controls();
        controls.Player.AddCallbacks(this);
        inventory ??= GetComponent<Inventory>();
        SwitchWeapon(inventory.GetWeapons()[0]);
    }
    
    public void OnMovement(InputAction.CallbackContext context) {

    }

    public void OnJump(InputAction.CallbackContext context) {

    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.action.triggered && currentWeapon != null) {
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