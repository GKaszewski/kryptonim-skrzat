using System;
using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField, Scene] private List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    [SerializeField] private CinemachineVirtualCamera activeCamera;
    [SerializeField] private Transform player;
    [SerializeField, Self] private Camera mainCamera;

    private void Start() {
        activeCamera.Priority = 10;
        SetLowPriority();
    }
    
    private void Update() {
        SwitchCameraIfNeeded();
    }
    
    private void SetLowPriority() {
        foreach (var cam in cameras) {
            if (cam == activeCamera) continue;
            cam.Priority = 0;
        }
    }
    
    private void SwitchCameraIfNeeded() {
        if (IsPlayerVisible()) return;
        var closestCamera = GetClosestCamera();
        if (closestCamera == activeCamera) return;
        
        activeCamera.Priority = 0;
        closestCamera.Priority = 10;
        activeCamera = closestCamera;
    }
    
    private bool IsPlayerVisible() {
        var viewportPoint = mainCamera.WorldToViewportPoint(player.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }
    
    private CinemachineVirtualCamera GetClosestCamera() {
        CinemachineVirtualCamera closestCamera = null;
        var minDistance = float.MaxValue;
        foreach (var cam in cameras) {
            var distance = Vector3.Distance(cam.transform.position, player.position);
            if (distance < minDistance) {
                minDistance = distance;
                closestCamera = cam;
            }
        }

        return closestCamera;
    }
}