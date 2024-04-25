using System;
using UnityEngine;

public class Bow : MonoBehaviour  {
    private float attackFireRateCounter;
    private bool detectedPlayer = false;
    
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float attackFireRate = 0.2f;
    [SerializeField] private int arrowIndex = 1;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private LayerMask playerLayer;

    private void Update() {
        if (attackFireRateCounter > 0f) {
            attackFireRateCounter -= Time.deltaTime;
        }
        
        DetectPlayer();
        if (CanAttack()) {
            Attack();
        }
    }

    private void Attack() {
        if (!(attackFireRateCounter <= 0f)) return;
        if (!ObjectPooler.SharedInstance.GetPooledObject(arrowIndex)) {
            return;
        }

        var arrow = ObjectPooler.SharedInstance.GetPooledObject(arrowIndex).GetComponent<MagicBolt>();
        if (!arrow) return; // this is unnecessary, but let's do a double check

        arrow.transform.position = arrowSpawnPoint.position;
        // set rotation to direction of the bow
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, transform.right.x > 0 ? 0f : 180f);
        arrow.gameObject.SetActive(true);
        arrow.Launch(transform.right);

        if (shootSound) AudioSource.PlayClipAtPoint(shootSound, transform.position);
        attackFireRateCounter = attackFireRate;
    }
    
    private void DetectPlayer() {
        var hit = Physics2D.Linecast(transform.position, transform.position + transform.right * detectionRange, playerLayer);
        detectedPlayer = hit; // any hit on player layer will be considered as detected player
    }
    
    private bool CanAttack() {
        return attackFireRateCounter <= 0f && detectedPlayer;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * detectionRange);
    }
}
