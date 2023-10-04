using System;
using System.Linq;
using Assets.WasapiAudio.Scripts.Unity;
using CSCore.Streams;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class WasapiBinder : AudioVisualizationEffect
    {
        [SerializeField] private AudioSpectrum audioSpectrum;

        public override void Update()
        {
            base.Update();
            
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