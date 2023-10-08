using System;
using DefaultNamespace;
using Lasp;
using UnityEngine;
using UnityEngine.Serialization;


public class FoVSyncer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera camera;
    [SerializeField] private WasapiBinder wasapiBinder;

    [Header("Audio Spectrum")] [SerializeField]
    private AnimationCurve fovCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private float fovMinValue = 50f;
    [SerializeField] private float fovMaxValue = 120f;
    
    
    private void Update()
    {

        float fov = (fovCurve.Evaluate(wasapiBinder.SmoothedMeanLevel) * (fovMaxValue - fovMinValue)) + fovMinValue;
        camera.fieldOfView = fov;
        
    }
}