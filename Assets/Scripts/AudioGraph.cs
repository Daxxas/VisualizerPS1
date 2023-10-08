using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;


public class AudioGraph : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float xStep = 0.01f;
    [SerializeField] private Vector2 nextPointPosition;
    [SerializeField] private WasapiBinder wasapiBinder;
    [SerializeField] private int maxPoints = 600;
    [SerializeField] private float yScale = 10f; 
    
    private void Start()
    {
        nextPointPosition = Vector2.zero;
    }

    private void Update()
    {
        nextPointPosition.x += xStep;
        nextPointPosition.y = wasapiBinder.CurrentLevel * yScale;
        AddPointToGraph(nextPointPosition);
    }

    private void AddPointToGraph(Vector2 position)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, position);
        
        if (lineRenderer.positionCount > maxPoints)
        {
            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            lineRenderer.SetPositions(ShiftPositions(positions));
            lineRenderer.positionCount = maxPoints;

        }
    }
    
    private Vector3[] ShiftPositions(Vector3[] positions)
    {
        Vector3[] shiftedPositions = new Vector3[positions.Length-1];
        for (int i = 0; i < positions.Length-1; i++)
        {
            shiftedPositions[i] = positions[i+1];
            shiftedPositions[i].x = i * xStep;
        }

        return shiftedPositions;
    }
}