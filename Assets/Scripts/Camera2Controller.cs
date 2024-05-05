using UnityEngine;
using KBCore.Refs;

public class Grid2DCameraTarget : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    [SerializeField]
    private Vector2 gridOffset;

    [SerializeField, Self]
    private Camera mainCamera;
    
    public Vector2 GridSize {
        get
        {
            if (!mainCamera) return new Vector2(float.NaN, float.NaN);
            float vh = mainCamera.orthographicSize * 2;
            float vw = vh * mainCamera.aspect;
            return new Vector2(vw, vh);
        }
    }

    private void Update()
    {
        UpdatePosition();
    }
    
    private Vector2 CalculateGridPosition(Vector2 targetPosition)
    {
        targetPosition -= gridOffset;
        Vector2 gridCell = targetPosition / GridSize;
        gridCell.x = Mathf.Floor(gridCell.x);
        gridCell.y = Mathf.Floor(gridCell.y);
        return gridCell * GridSize + gridOffset + GridSize / 2;
    }
    
    private void UpdatePosition()
    {
        Vector2 newPosition = CalculateGridPosition(followTarget.position);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        if (mainCamera == null) return;
        Gizmos.color = Color.white;
        int halfIters = 10; 
        float halfSizeX = GridSize.x * (halfIters+1);
        float halfSizeY = GridSize.y * (halfIters+1);
        for (int i = -halfIters; i <= halfIters; i++)
        {
            Vector2 center = CalculateGridPosition(transform.position);
            float lineX = center.x + i * GridSize.x - GridSize.x / 2;
            float lineY = center.y + i * GridSize.y - GridSize.y / 2;
            Gizmos.DrawLine(new Vector3(lineX, center.y - halfSizeY, 0), new Vector3(lineX, center.y + halfSizeY, 0));
            Gizmos.DrawLine(new Vector3(center.x - halfSizeX, lineY, 0), new Vector3(center.x + halfSizeX, lineY, 0));
        }
    }
}