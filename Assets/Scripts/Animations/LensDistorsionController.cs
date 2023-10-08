using DefaultNamespace;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;
using UnityEngine.Serialization;


public class LensDistorsionController : MonoBehaviour
{
    [SerializeField] private MMF_Player player;
    [SerializeField] private WasapiBinder wasapiBinder;

    [SerializeField] private float maxDistorsion = 0.5f;
    [SerializeField] private float maxGain = 10f;
    
    private MMF_LensDistortion_URP lensDistortionUrp;

    private void Start()
    {
        lensDistortionUrp = player.GetFeedbackOfType<MMF_LensDistortion_URP>();
    }

    private void Update()
    {
        float distorsion = (60f - wasapiBinder.NormalizedInput) * maxDistorsion / maxGain;
        lensDistortionUrp.RemapIntensityOne = distorsion;
    }
}