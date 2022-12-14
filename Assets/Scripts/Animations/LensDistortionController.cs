using Lasp;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

public class LensDistortionController : MonoBehaviour
{
    [SerializeField] private MMF_Player player;
    [SerializeField] private AudioLevelTracker levelTracker;
    [SerializeField] private float intensityOneDiviser = 1f;
    
    
    private void Start()
    {
        player = GetComponent<MMF_Player>();
        var lensDistortion = player.GetFeedbackOfType<MMF_LensDistortion_URP>();
        
        lensDistortion.RemapIntensityOne = levelTracker.currentGain / intensityOneDiviser;
    }
}