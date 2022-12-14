using System.Collections;
using System.Collections.Generic;
using Lasp;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public float spectrumValue;
     public float SpectrumValue { get => spectrumValue; set => spectrumValue = value; }
    
    

    // Update is called once per frame
    void Update()
    {
        // AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);
        
        // if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        // {
        //     spectrumValue = m_audioSpectrum[0] * 100;
        // }

        // Debug.Log(spectrumValue);
    }
}
