using UnityEngine;

public class PlayerDeathBehavior : MonoBehaviour, IDeathBehavior {
    public void Die() {
        GameManager.Instance.RestartGame();
    }
}