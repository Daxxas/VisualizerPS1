using System;
using Lasp;
using UnityEngine;


public class FoVSyncer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera camera;
    [SerializeField] private AudioSpectrum audioSpectrum;
    
    [Header("Audio Spectrum")] 
    [SerializeField] private float spectrumMinValue = 50f;
    [SerializeField] private float spectrumMaxValue = 120f;
    
    
    private void Update()
    {
        
        float fov =  Mathf.Lerp(spectrumMinValue, spectrumMaxValue, audioSpectrum.SpectrumMeanValue/100f);
        // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
        camera.fieldOfView = fov;
        
    }
}