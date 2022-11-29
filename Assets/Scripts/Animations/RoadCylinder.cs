using System;
using Lasp;
using UnityEngine;

public class RoadCylinder : MonoBehaviour
{
    [SerializeField] private AudioLevelTracker audioLevelTracker;
    [SerializeField] private Animator animator;

    [SerializeField] private float speedCoefficient = 1.1f;
    [SerializeField] private float speedDeltaCoefficient = 1.1f;
    
    private void Update()
    {
        float turnMultiplier = (((Mathf.Pow(audioLevelTracker.currentGain, speedDeltaCoefficient))) / 100f) * speedCoefficient;
        Debug.Log("Turn Multiplier : " + turnMultiplier);
        animator.SetFloat("turnMultiplier", turnMultiplier);
    }
}