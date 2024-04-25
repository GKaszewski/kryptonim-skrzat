using UnityEngine;

public class EnemyDeathBehavior : MonoBehaviour, IDeathBehavior {
    [SerializeField] private int scoreAmount = 10;
    public void Die() {
        GameManager.Instance.IncreaseScore(scoreAmount);
        Destroy(gameObject);
    }
}