using UnityEngine;

public class ResetComponentAfterTime : MonoBehaviour {
    private Vector3 lastPosition;
    private float timer;
    
    [SerializeField] private float idleTime = 5.0f;
    [SerializeField] private MonoBehaviour componentToReset;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool resetOnStart;
    
    private void Start() {
        lastPosition = transform.position;
        
        if (resetOnStart) {
            ResetComponent();
        }
    }
    
    private void Update() {
        timer += Time.deltaTime;
        if (timer >= idleTime) {
            CheckIfIdle();
        }
        
        if (isIdle) {
            Debug.Log("Resetting component...");
            ResetComponent();
        }
    }

    private void CheckIfIdle() {
        if (lastPosition == transform.position) {
            isIdle = true;
        } else {
            lastPosition = transform.position;
            timer = 0;
        }
    }
    
    private void ResetComponent() {
        componentToReset.enabled = false;
        componentToReset.enabled = true;
        timer = 0;
        isIdle = false;
    }
}