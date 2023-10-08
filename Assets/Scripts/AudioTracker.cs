using System.Collections;
using System.Collections.Generic;
using Lasp;
using UnityEngine;

public class AudioTracker : MonoBehaviour
{
     public float spectrumValue;
     //public float SpectrumValue { get => spectrumValue; set => spectrumValue = value; }

     public float smoothPeriod = .5f;
     
     private float spectrumMeanValue;
     public float SpectrumMeanValue => spectrumMeanValue;

     public void UpdateSpectrumMeanValue()
     {
         // calculate mean value of spectrum over time
         spectrumMeanValue = Mathf.Lerp(spectrumMeanValue, spectrumValue, Time.deltaTime / smoothPeriod);
     }
     
    // Update is called once per frame
    void Update()
    {
        UpdateSpectrumMeanValue();
        // AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        // if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        // {
        //     spectrumValue = m_audioSpectrum[0] * 100;
        // }

        // Debug.Log(spectrumValue);
    }
}
