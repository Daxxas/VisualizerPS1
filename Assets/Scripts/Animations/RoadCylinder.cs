using System;
using DefaultNamespace;
using Lasp;
using UnityEngine;
using UnityEngine.Serialization;

public class RoadCylinder : MonoBehaviour
{
    [FormerlySerializedAs("audioTracker")] [SerializeField] private WasapiBinder wasapiBinder;
    [SerializeField] private Animator animator;

    [Header("Audio Spectrum")] 
    [SerializeField] private AnimationCurve turnCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private float turnFactor = 1f;

    private void Update()
    {
        float turnMultiplier = turnCurve.Evaluate(wasapiBinder.CurrentLevel) * turnFactor;

        turnMultiplier = -Mathf.Abs(turnMultiplier);
        // Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
        animator.SetFloat("turnMultiplier", turnMultiplier);
    }
}