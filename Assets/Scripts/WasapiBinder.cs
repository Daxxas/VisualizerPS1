using System;
using Assets.WasapiAudio.Scripts.Core;
using Assets.WasapiAudio.Scripts.Unity;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class WasapiBinder : AudioVisualizationEffect
    {
        [SerializeField] private AudioSpectrum audioSpectrum;
        [SerializeField] private bool autoGain = true;
        
        
        private void Update()
        {
            float[] spectrumData = GetSpectrumData();
            // Mean of spectrum data
            float spectrumMeanValue = 0;
            foreach (float spectrumValue in spectrumData)
            {
                spectrumMeanValue += spectrumValue;
            }
            spectrumMeanValue /= spectrumData.Length;
            
            audioSpectrum.SpectrumValue = spectrumMeanValue;
            
             
            
            if (autoGain)
            {
                // // Slowly return to the noise floor.
                // const float kDecaySpeed = 0.6f;
                // _head = Mathf.Max(_head - kDecaySpeed * dt, kSilence);
                //
                // // Pull up by input with a small headroom.
                // var room = _dynamicRange * 0.05f;
                // _head = Mathf.Clamp(input - room, _head, 0);
            }
            
        }
    }
}