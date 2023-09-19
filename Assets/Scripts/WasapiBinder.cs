using System;
using Assets.WasapiAudio.Scripts.Core;
using Assets.WasapiAudio.Scripts.Unity;
using UnityEngine;

namespace DefaultNamespace
{
    public class WasapiBinder : AudioVisualizationEffect
    {
        [SerializeField] private AudioSpectrum audioSpectrum;

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
            
        }
    }
}