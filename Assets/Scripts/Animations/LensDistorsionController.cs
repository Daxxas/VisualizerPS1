using System;
using Lasp;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;


public class LensDistorsionController : MonoBehaviour
{
    [SerializeField] private MMF_Player player;
    [SerializeField] private AudioLevelTracker audioLevelTracker;

    [SerializeField] private float maxDistorsion = 0.5f;
    [SerializeField] private float maxGain = 10f;
    
    private MMF_LensDistortion_URP lensDistortionUrp;

    private void Start()
    {
        lensDistortionUrp = player.GetFeedbackOfType<MMF_LensDistortion_URP>();
    }

    private void Update()
    {
        float distorsion = (60f - audioLevelTracker.inputLevel) * maxDistorsion / maxGain; 
        // Debug.Log(distorsion + " - " + audioLevelTracker.inputLevel);
        lensDistortionUrp.RemapIntensityOne = distorsion;
    }
}