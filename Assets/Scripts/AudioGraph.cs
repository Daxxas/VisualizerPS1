using System;
using UnityEngine;


public class AudioGraph : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float xStep = 0.01f;
    [SerializeField] private Vector2 nextPointPosition;

    private void Start()
    {
        nextPointPosition = Vector2.zero;
    }

    private void Update()
    {
        nextPointPosition.x += xStep;
        nextPointPosition.y = AudioSpectrum.SpectrumValue;
        AddPointToGraph(nextPointPosition);
    }

    private void AddPointToGraph(Vector2 position)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, position);
    }
}