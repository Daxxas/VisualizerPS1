using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Parent class responsible for extracting beats from..
/// ..spectrum value given by AudioSpectrum.cs
/// </summary>
public class AudioSyncer : MonoBehaviour
{
    public float bias = 80f;
    public float timeStep = .05f;
    public float timeToBeat = .05f;
    public float restSmoothTime;

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;

    protected bool m_isBeat;

    [SerializeField] private bool debugMode = false;
    [SerializeField] private WasapiBinder wasapiBinder;
    public WasapiBinder WasapiBinder => wasapiBinder;

    /// <summary>
    /// Inherit this to cause some behavior on each beat
    /// </summary>
    public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }

    /// <summary>
    /// Inherit this to do whatever you want in Unity's update function
    /// Typically, this is used to arrive at some rest state..
    /// ..defined by the child class
    /// </summary>
    public virtual void OnUpdate()
    { 
        m_isBeat = false;
        // update audio value
        m_previousAudioValue = m_audioValue;
        m_audioValue = wasapiBinder.NormalizedInput;

        // if audio value went below the bias during this frame
        if (m_previousAudioValue > bias &&
            m_audioValue <= bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        // if audio value went above the bias during this frame
        if (m_previousAudioValue <= bias &&
            m_audioValue > bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        m_timer += Time.deltaTime;
    }

    public virtual void Update()
    {
        OnUpdate();
    }

    int lastBeat = 0;
    
    private void OnGUI()
    {
        if (!debugMode)
            return;
        
        GUI.Label(new Rect(10, 10, 200, 20), "NormalizedInput: " + m_audioValue.ToString());

        if (m_isBeat)
        {
            lastBeat = Time.frameCount;
        }
        GUI.Label(new Rect(10, 30, 200, 20), "Beat: " + lastBeat);
    }
}