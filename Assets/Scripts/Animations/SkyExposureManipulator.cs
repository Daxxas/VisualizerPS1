using System;
using UnityEngine;


public class SkyExposureManipulator : MonoBehaviour
{
    [SerializeField] public float _exposure = 1.0f;
    
    public float Exposure
    {
        get => _exposure;
        set => _exposure = value;
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_contrastGradient", _exposure);
    }
}