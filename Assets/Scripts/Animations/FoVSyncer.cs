using System;
using Lasp;
using UnityEngine;


public class FoVSyncer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera camera;
    [SerializeField] private AudioSpectrum audioSpectrum;
    
    [Header("Audio Spectrum")] 
    [SerializeField] private float fovMinValue = 50f;
    [SerializeField] private float fovMaxValue = 120f;
    [SerializeField] private float meanValueScale = 1f;
    
    
    private void Update()
    {
        
        float fov =  Mathf.Lerp(fovMinValue, fovMaxValue, audioSpectrum.SpectrumMeanValue/meanValueScale);
        // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
        camera.fieldOfView = fov;
        
    }
}