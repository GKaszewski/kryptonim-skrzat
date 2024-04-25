using UnityEngine;

public interface IWeapon {
    void Initialize(Transform spawnPoint);
    void Attack();
}