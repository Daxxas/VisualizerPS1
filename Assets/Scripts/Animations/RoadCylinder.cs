using System;
using Lasp;
using UnityEngine;

public class RoadCylinder : MonoBehaviour
{
    [SerializeField] private AudioSpectrum audioSpectrum;
    [SerializeField] private Animator animator;

    [Header("Audio Spectrum")] 
    [SerializeField] private float spectrumMinValue = 0f;
    [SerializeField] private float spectrumMaxValue = 1f;

    private void Update()
    {
       
        float turnMultiplier =  Mathf.Lerp(spectrumMinValue, spectrumMaxValue, audioSpectrum.SpectrumMeanValue/100f);

        turnMultiplier = -Mathf.Abs(turnMultiplier);
        // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
        animator.SetFloat("turnMultiplier", turnMultiplier);
    }
}