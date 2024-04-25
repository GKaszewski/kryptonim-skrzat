using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MagicBoltWeapon : MonoBehaviour, IWeapon {
    private float attackFireRateCounter;
    [SerializeField] private Transform magicBoltSpawnPoint;

    [SerializeField] private WeaponData weaponData;
    public string Name => weaponData.weaponName;

    public void Initialize(Transform spawnPoint) {
        magicBoltSpawnPoint = spawnPoint;
    }

    public void Attack() {
        if (attackFireRateCounter <= 0f) {
            var magicBolt = ObjectPooler.SharedInstance.GetPooledObject(weaponData.prefabIndex).GetComponent<MagicBolt>();
            if (!magicBolt) return;
            
            magicBolt.transform.position = magicBoltSpawnPoint.position;
            magicBolt.transform.rotation = Quaternion.identity;
            magicBolt.gameObject.SetActive(true);
            magicBolt.Launch(transform.right);
            if(weaponData.attackSound) AudioSource.PlayClipAtPoint(weaponData.attackSound, transform.position);
            attackFireRateCounter = weaponData.attackRate;
        }
    }

    private void Update() {
        if (attackFireRateCounter > 0f) {
            attackFireRateCounter -= Time.deltaTime;
        }
    }
}