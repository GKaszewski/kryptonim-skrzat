using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour {
    private Bounds cameraBounds;
    private float distanceToLeftEdge;
    private float distanceToRightEdge;
    private float distanceToTopEdge;
    private float distanceToBottomEdge;

    private bool isNearLeftEdge;
    private bool isNearRightEdge;
    private bool isNearTopEdge;
    private bool isNearBottomEdge;
    
    [SerializeField, Self] private PixelPerfectCamera pixelPerfectCamera;
    [SerializeField] private CinemachineVirtualCamera activeCamera;
    [SerializeField] private Transform player;
    [SerializeField, Self] private Camera mainCamera;
    [SerializeField] private int tileSize = 16;

    private void Start() {
        activeCamera.Priority = 10;
        CalculateBounds();
        CalculateEdgeDistances();
    }
    
    private void LateUpdate() {
        SwitchCameraIfNeeded();
    }
    
    private void SwitchCameraIfNeeded() {
        if (IsPlayerVisible()) return;
        CalculateEdgeDistances();
        CheckIfNearEdges();

        if ((isNearLeftEdge && player.position.x < cameraBounds.min.x) || (
                isNearRightEdge && player.position.x > cameraBounds.max.x)) {
            if (isNearLeftEdge) activeCamera.transform.position -= new Vector3(tileSize, 0, 0);
            else if (isNearRightEdge) activeCamera.transform.position += new Vector3(tileSize, 0, 0);
            
            CalculateBounds();
        }
        
        if ((isNearTopEdge && player.position.y > cameraBounds.max.y) || (
                isNearBottomEdge && player.position.y < cameraBounds.min.y)) {
            if (isNearTopEdge) activeCamera.transform.position += new Vector3(0, tileSize, 0);
            else if (isNearBottomEdge) activeCamera.transform.position -= new Vector3(0, tileSize, 0);
            
            CalculateBounds();
        }
    }
    
    private bool IsPlayerVisible() {
        var viewportPoint = mainCamera.WorldToViewportPoint(player.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }

    private void CalculateBounds() {
        var cameraPosition = mainCamera.transform.position;
        var tilesX = pixelPerfectCamera.refResolutionX / tileSize;
        var tilesY = pixelPerfectCamera.refResolutionY / tileSize;
        
        var minX = cameraPosition.x - tilesX / 2;
        var maxX = cameraPosition.x + tilesX / 2;
        var minY = cameraPosition.y - tilesY / 2;
        var maxY = cameraPosition.y + tilesY / 2;
        
        cameraBounds.center = new Vector3(cameraPosition.x, cameraPosition.y, 0);
        cameraBounds.size = new Vector3(maxX - minX, maxY - minY, 0);
    }
    
    private void CalculateEdgeDistances() {
        distanceToLeftEdge = player.position.x - cameraBounds.min.x;
        distanceToRightEdge = cameraBounds.max.x - player.position.x;
        distanceToTopEdge = cameraBounds.max.y - player.position.y;
        distanceToBottomEdge = player.position.y - cameraBounds.min.y;
    }
    
    private void CheckIfNearEdges() {
        isNearLeftEdge = distanceToLeftEdge < tileSize * 2;
        isNearRightEdge = distanceToRightEdge < tileSize * 2;
        isNearTopEdge = distanceToTopEdge < tileSize * 2;
        isNearBottomEdge = distanceToBottomEdge < tileSize * 2;
    }
}