using UnityEngine;
using UnityEngine.Events;

public class MusicEventSyncer : AudioSyncer
{
    
    [SerializeField] private UnityEvent onEvent;

    [SerializeField] private float meanDelta = 20f;
    [SerializeField] private float meanMinimum = 50f;
    [SerializeField] private float meanDeltaSmoothPeriod = .1f;
    
    private bool eventOnNextBeat = false;

    private float meanValuePreviousFrame = 0f;
    private float deltaMean = 0f;
    private float maxDeltaMean = 0f;
    
    public override void Update()
    {
        base.Update();
        
        float frameDelta = AudioSpectrum.SpectrumMeanValue - meanValuePreviousFrame;
        deltaMean = Mathf.Lerp(deltaMean, frameDelta, Time.deltaTime / meanDeltaSmoothPeriod);

        // calculate max delta mean
        if (deltaMean > maxDeltaMean)
        {
            maxDeltaMean = deltaMean;
        }
        
        if(deltaMean > meanDelta)
        {
            eventOnNextBeat = true;
        }
        
        // Debug.Log(deltaMean + " " + eventOnNextBeat);

        meanValuePreviousFrame = AudioSpectrum.SpectrumMeanValue;
    }

    [ContextMenu("Reset Max")]
    public void ResetMax()
    {
        maxDeltaMean = 0f;
    }
    
    public override void OnBeat()
    {
        base.OnBeat();
        if (eventOnNextBeat)
        {
            onEvent?.Invoke();
            Debug.Log("Event !");
            eventOnNextBeat = false;
        }
    }
}
