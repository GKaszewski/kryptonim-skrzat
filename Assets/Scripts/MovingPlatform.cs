using System;
using KBCore.Refs;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    private int currentWaypointIndex;
    private Vector3 targetPosition;
    private float waitTimer;
    
    [SerializeField, Self] private Rigidbody2D rb;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private bool isLooping = true;
    [SerializeField] private float minDistance = 0.1f;

    private void OnDisable() {
        foreach (Transform child in transform) {
            if (child.CompareTag("Player")) {
                child.SetParent(null);
            }
        }
    }

    private void Start() {
        if (waypoints.Length == 0) {
            Debug.LogError("No waypoints assigned to the moving platform.");
            return;
        }
        
        transform.position = waypoints[0].position;
        currentWaypointIndex = 1;
    }

    private void Update() {
        if (Vector2.Distance(transform.position, targetPosition) < minDistance) {
            waitTimer += Time.deltaTime;
        }
        
        if (waitTimer >= waitTime) {
            waitTimer = 0f;
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) {
                if (isLooping) {
                    currentWaypointIndex = 0;
                } else {
                    currentWaypointIndex = waypoints.Length - 1;
                }
            }
        }
    }

    private void LateUpdate() {
        MoveToWaypoint();
    }

    private void MoveToWaypoint() {
        targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y) {
            other.transform.SetParent(transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && other.transform.parent == transform) {
            other.transform.SetParent(null);
        }
    }
}