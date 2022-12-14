using System;
using Lasp;
using UnityEngine;

public class RoadCylinder : MonoBehaviour
{
    [SerializeField] private AudioLevelTracker audioLevelTracker;
    [SerializeField] private AudioSpectrum audioSpectrum;
    [SerializeField] private bool useAudioSpectrum = false;
    [SerializeField] private Animator animator;

    
    [Header("Audio Level Tracker")]
    [SerializeField] private float speedCoefficient = 1.1f;
    [SerializeField] private float speedDeltaCoefficient = 1.1f;
    [SerializeField] private float offset = 60f;
    [SerializeField] private bool clamp = true;
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;

    [Header("Audio Spectrum")] 
    [SerializeField] private float smoothPeriod = .5f; 
    [SerializeField] private float spectrumMinValue = 0f;
    [SerializeField] private float spectrumMaxValue = 1f;

    private float spectrumMeanValue;
    
    private void Update()
    {
        if (!useAudioSpectrum)
        {
            float turnMultiplier =  offset - ((audioLevelTracker.currentGain / speedCoefficient) * speedDeltaCoefficient);

            if (clamp)
            {
                turnMultiplier = Mathf.Clamp(turnMultiplier, minValue, maxValue);
            }
            
            turnMultiplier = -Mathf.Abs(turnMultiplier);
            // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
            animator.SetFloat("turnMultiplier", turnMultiplier);
        }
        else
        {
            CalculateSpectrumMeanValue();
            
            float turnMultiplier =  Mathf.Lerp(spectrumMinValue, spectrumMaxValue, spectrumMeanValue/100f);

            turnMultiplier = -Mathf.Abs(turnMultiplier);
            // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
            animator.SetFloat("turnMultiplier", turnMultiplier);
        }
    }

    private void CalculateSpectrumMeanValue()
    {
        // calculate mean value of spectrum over time
        spectrumMeanValue = Mathf.Lerp(spectrumMeanValue, audioSpectrum.spectrumValue, Time.deltaTime / smoothPeriod);
    }
}