using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MusicEventSyncer : AudioSyncer
{
    [SerializeField] private float meanTriggerDelta = 20f;
    [SerializeField] private int meanBeat = 4;
    [SerializeField] private int eventBeatCooldown = 64;
    [Header("Random Events")]
    [SerializeField] private List<MusicEvent> musicEvents = new List<MusicEvent>();
    [SerializeField] private int randomEventHistoryLength = 4;
    
    private bool eventOnNextBeat = false;

    private float meanValuePreviousFrame = 0f;
    private float deltaMean = 0f;
    private float maxDeltaMean = 0f;
    
    private float[] lastBeatValue;

    private int currrentCooldown = 0;
    
    
    ////////////
    private List<int> lastEventsIndex = new List<int>();
    
    private void Start()
    {
        lastBeatValue = new float[meanBeat];
        
        for (int i = 0; i < meanBeat; i++)
        {
            lastBeatValue[i] = 0f;
        }
    }

    [ContextMenu("Reset Max")]
    public void ResetMax()
    {
        maxDeltaMean = 0f;
    }
    
    public override void OnBeat()
    {
        base.OnBeat();
        
        currrentCooldown++;
        
        if (currrentCooldown < eventBeatCooldown)
        {
            return;
        }

        float beatDelta = Mathf.Abs(AudioSpectrum.SpectrumMeanValue - GetMeanValueOfLastBeats());
        
        if (beatDelta > meanTriggerDelta)
        {
            currrentCooldown = 0;
            StartRandomEvent();
            Debug.Log("Event !");
            eventOnNextBeat = false;
        }
        
        
        // Store last beat values
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
    
    
    public void StartRandomEvent()
    {
        int eventIndex = UnityEngine.Random.Range(0, musicEvents.Count);

        if (musicEvents.Count > randomEventHistoryLength)
        {
            while (lastEventsIndex.Contains(eventIndex))
            {
                eventIndex = UnityEngine.Random.Range(0, musicEvents.Count);
            }
        }
        
        lastEventsIndex.Add(eventIndex);
        
        if (lastEventsIndex.Count > randomEventHistoryLength)
        {
            lastEventsIndex.RemoveAt(0);
        }
        
        musicEvents[eventIndex].StartEvent();
    }
}
