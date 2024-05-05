using Cinemachine;
using KBCore.Refs;
using UnityEngine;

public class Grid2DCameraTarget : MonoBehaviour {
    [SerializeField]
    private Transform followTarget;

    [SerializeField]
    private Vector2 gridOffset;

    [SerializeField, Self]
    private Camera mainCamera;

    [SerializeField, Scene]
    private CinemachineVirtualCamera activeCamera;

    public Vector2 GridSize {
        get {
            if (!mainCamera) return new Vector2(float.NaN, float.NaN);
            var vh = mainCamera.orthographicSize * 2;
            var vw = vh * mainCamera.aspect;
            return new Vector2(vw, vh);
        }
    }

    private void Update() {
        UpdatePosition();
    }

    private Vector2 CalculateGridPosition(Vector2 targetPosition) {
        targetPosition -= gridOffset;
        var gridCell = targetPosition / GridSize;
        gridCell.x = Mathf.Floor(gridCell.x);
        gridCell.y = Mathf.Floor(gridCell.y);
        return gridCell * GridSize + gridOffset + GridSize / 2;
    }

    private void UpdatePosition() {
        var newPosition = CalculateGridPosition(followTarget.position);
        activeCamera.transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private void OnDrawGizmosSelected() {
        if (mainCamera == null) return;
        Gizmos.color = Color.yellow;
        var halfIters = 10;
        var halfSizeX = GridSize.x * (halfIters + 1);
        var halfSizeY = GridSize.y * (halfIters + 1);
        for (var i = -halfIters; i <= halfIters; i++) {
            Vector2 center = CalculateGridPosition(transform.position);
            var lineX = center.x + i * GridSize.x - GridSize.x / 2;
            var lineY = center.y + i * GridSize.y - GridSize.y / 2;
            Gizmos.DrawLine(new Vector3(lineX, center.y - halfSizeY, 0), new Vector3(lineX, center.y + halfSizeY, 0));
            Gizmos.DrawLine(new Vector3(center.x - halfSizeX, lineY, 0), new Vector3(center.x + halfSizeX, lineY, 0));
        }
    }
}