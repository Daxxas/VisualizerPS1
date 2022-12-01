using System;
using Lasp;
using UnityEngine;

public class RoadCylinder : MonoBehaviour
{
    [SerializeField] private AudioLevelTracker audioLevelTracker;
    [SerializeField] private Animator animator;

    [SerializeField] private float speedCoefficient = 1.1f;
    [SerializeField] private float speedDeltaCoefficient = 1.1f;
    [SerializeField] private float offset = 60f;
    [SerializeField] private bool clamp = true;
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;
    
    
    private void Update()
    {
        float turnMultiplier =  offset - ((audioLevelTracker.currentGain / speedCoefficient) * speedDeltaCoefficient);

        if (clamp)
        {
            turnMultiplier = Mathf.Clamp(turnMultiplier, minValue, maxValue);
        }
        
        turnMultiplier = -Mathf.Abs(turnMultiplier);
        Debug.Log("Turn Multiplier : " + turnMultiplier + " | audioLevelTracker.currentGain : " + audioLevelTracker.currentGain);
        animator.SetFloat("turnMultiplier", turnMultiplier);
    }
}