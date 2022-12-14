using System;
using Lasp;
using UnityEngine;


public class FoVSyncer : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private AudioLevelTracker audioLevelTracker;
    [SerializeField] private AudioSpectrum audioSpectrum;
    [SerializeField] private float baseFov = 60f;
    [SerializeField] private float fovCoefficient = 1f;
    [SerializeField] private float fovDeltaCoefficient = 0.125f;
    [SerializeField] private float minFov = 40f;
    [SerializeField] private float maxFov = 120f;


    [Header("Audio Spectrum")] 
    [SerializeField] private bool useAudioSpectrum = false;
    [SerializeField] private float smoothPeriod = .5f; 
    [SerializeField] private float spectrumMinValue = 50f;
    [SerializeField] private float spectrumMaxValue = 120f;
    
    private float spectrumMeanValue;
    
    private void Update()
    {
        if (!useAudioSpectrum)
        {
            float fov =  baseFov - ((audioLevelTracker.currentGain / fovCoefficient) * fovDeltaCoefficient);
            // Debug.Log("fov : " + fov + " | " + baseFov + " * (((" + "60f - " + audioLevelTracker.currentGain + ") * " + fovDeltaCoefficient + ") / " + fovCoefficient+ ")");

            fov = Mathf.Clamp(fov, minFov, maxFov);
            
            camera.fieldOfView = fov;
        }
        else
        {
            CalculateSpectrumMeanValue();
                
            float fov =  Mathf.Lerp(spectrumMinValue, spectrumMaxValue, spectrumMeanValue/100f);
            Debug.Log(fov);
            // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
            camera.fieldOfView = fov;
            
        }
        
        
        
    }
    
    
    private void CalculateSpectrumMeanValue()
    {
        // calculate mean value of spectrum over time
        spectrumMeanValue = Mathf.Lerp(spectrumMeanValue, audioSpectrum.spectrumValue, Time.deltaTime / smoothPeriod);
    }
}