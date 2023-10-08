using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MusicEventSyncer : AudioSyncer
{
    
    [SerializeField] private UnityEvent onEvent;

    [SerializeField] private float meanTriggerDelta = 20f;
    [SerializeField] private int meanBeat = 4;
    
    private bool eventOnNextBeat = false;

    private float meanValuePreviousFrame = 0f;
    private float deltaMean = 0f;
    private float maxDeltaMean = 0f;
    
    private float[] lastBeatValue;

    private void Start()
    {
        lastBeatValue = new float[meanBeat];
        
        for (int i = 0; i < meanBeat; i++)
        {
            lastBeatValue[i] = 0f;
        }
    }

    public override void Update()
    {
        base.Update();
        
        // float frameDelta = AudioSpectrum.SpectrumMeanValue - meanValuePreviousFrame;
        // deltaMean = Mathf.Lerp(deltaMean, frameDelta, Time.deltaTime / meanDeltaSmoothPeriod);
        //
        // // calculate max delta mean
        // if (deltaMean > maxDeltaMean)
        // {
        //     maxDeltaMean = deltaMean;
        // }
        //
        // if(deltaMean > meanTriggerDelta)
        // {
        //     eventOnNextBeat = true;
        //     Debug.Log("Event !");
        // }
        //
        // Debug.Log(deltaMean + " > " + meanTriggerDelta + " " + AudioSpectrum.SpectrumMeanValue +  " " + eventOnNextBeat);
        //
        // meanValuePreviousFrame = AudioSpectrum.SpectrumMeanValue;
    }

    [ContextMenu("Reset Max")]
    public void ResetMax()
    {
        maxDeltaMean = 0f;
    }
    
    public override void OnBeat()
    {
        base.OnBeat();

        float beatDelta = Mathf.Abs(AudioSpectrum.SpectrumMeanValue - GetMeanValueOfLastBeats());
        
        if (beatDelta > meanTriggerDelta)
        {
            Debug.Log("Event!");
            eventOnNextBeat = true;
        }

        Debug.Log(beatDelta + " > " + meanTriggerDelta);
        
        if (eventOnNextBeat)
        {
            onEvent?.Invoke();
            Debug.Log("Event !");
            eventOnNextBeat = false;
        }
        
        for (int i = 0; i < meanBeat - 1; i++)
        {
            lastBeatValue[i] = lastBeatValue[i + 1];
        }
        lastBeatValue[meanBeat - 1] = AudioSpectrum.SpectrumMeanValue;
    }

    private float GetMeanValueOfLastBeats()
    {
        float total = 0f;

        for (int i = 0; i < lastBeatValue.Length; i++)
        {
            total += lastBeatValue[i];
        }
        
        return total/lastBeatValue.Length;
    }
}
