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
        private float spectrumValue;
        public float SpectrumValue { get => spectrumValue; set => spectrumValue = value; }

        public float smoothPeriod = .5f;
     
        private float spectrumMeanValue;
        public float SpectrumMeanValue => spectrumMeanValue;
        
        public override void Update()
        {
            base.Update();
            
            float[] spectrumData = GetSpectrumData();
            // Mean of spectrum data
            float spectrumArrayMeanValue = 0;
            
            foreach (float interationSpectrumValue in spectrumData)
            {
                spectrumArrayMeanValue += interationSpectrumValue;
            }
            spectrumArrayMeanValue /= spectrumData.Length;
            
            spectrumValue = spectrumArrayMeanValue;
            spectrumMeanValue = Mathf.Lerp(spectrumArrayMeanValue, spectrumValue, Time.deltaTime / smoothPeriod);
        }
    }
}