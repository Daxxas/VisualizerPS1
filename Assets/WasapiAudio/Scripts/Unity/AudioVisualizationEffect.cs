using System;
using System.Collections.Generic;
using System.Linq;
using Assets.WasapiAudio.Scripts.Core;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.WasapiAudio.Scripts.Unity
{
    public abstract class AudioVisualizationEffect : MonoBehaviour
    {
        private float[] _spectrumData;
        private float _currentLevel = 0.0f;
        private float _smoothedLevel = 0.0f;
        private float _input = 0.0f;
        private float _clampedInput = 0.0f;
        private float _normalizedInput = 0.0f;
        private float smoothedMeanLevel = 0.0f;
        
        // Inspector Properties
        public WasapiAudioSource WasapiAudioSource;
        public int SpectrumSize = 32;
        public ScalingStrategy ScalingStrategy = ScalingStrategy.Sqrt;
        public WindowFunctionType WindowFunctionType = WindowFunctionType.BlackmannHarris;
        public int MinFrequency = 100;
        public int MaxFrequency = 20000;

        [SerializeReference]
        public List<SpectrumTransformer> Transformers = new();

        [SpectrumDataPreview]
        public SpectrumData Preview;

        [FormerlySerializedAs("meanLevelSmoothTime")]
        [Header("Mean Level Settings")]
        [SerializeField] private float meanLevelSmoothSharpness = 0.5f;
        
        [Header("Auto Gain Settings")]
        [SerializeField] private bool autoGain = true;
        [SerializeField] private float dynamicRange = .2f;
        [SerializeField] private float decaySpeed = .2f;
        [SerializeField] private bool forceSmoothFall = true;
        [SerializeField] private float smoothFall = 1;

        [Header("Debug")] 
        [SerializeField] private bool debugDisplay = false;
        
        
        protected bool IsIdle => _spectrumData?.All(v => v < 0.001f) ?? true;

        private PeakMeter peakMeter;
        
        public float CurrentLevel
        {
            get
            {
                _currentLevel = GetLevel();
                return _currentLevel;
            }
        }

        public float SmoothedLevel => _smoothedLevel;
        public float Input => _input;
        public float ClampedInput => _clampedInput;
        public float NormalizedInput => _normalizedInput;

        public float SmoothedMeanLevel => smoothedMeanLevel;

        public float DynamicRange => dynamicRange;
        
        private float _normalizedLevel = 0.0f;
        public virtual void Awake()
        {
            if (WasapiAudioSource == null)
            {
                Debug.Log("You must set a WasapiAudioSource");
                return;
            }

            var receiver = new SpectrumReceiver(SpectrumSize, ScalingStrategy, WindowFunctionType, MinFrequency,
                MaxFrequency, spectrumData =>
                {
                    _spectrumData = spectrumData;
                });
            
            WasapiAudioSource.AddReceiver(receiver);
            
            Preview = new SpectrumData();
        }

        protected float GetLevel()
        {
            if (!Application.isPlaying)
                return 0;
            
            float peakValue = AudioMeterInformation.FromDevice(WasapiAudioSource.WasapiAudio.WasapiCapture.Device).GetPeakValue();
            return peakValue;
        }
        
        protected float[] GetSpectrumData()
        {
            if (WasapiAudioSource == null)
            {
                Debug.Log("You must set a WasapiAudioSource");
                return null;
            }

            // Get raw / unmodified spectrum data
            var spectrumData = _spectrumData;

            // Run spectrum data through all configured transformers
            if (Transformers != null && Transformers.Count > 0)
            {
                foreach (var transformer in Transformers)
                {
                    spectrumData = transformer.Transform(spectrumData);
                }
            }

            Preview.Values = spectrumData;

            return spectrumData;
        }

        public virtual void Update()
        {
            if (autoGain)
            {
                _currentLevel = GetLevel();
                
                if(_currentLevel > _smoothedLevel)
                    _smoothedLevel = _currentLevel;
                else
                    _smoothedLevel = Mathf.Lerp(_smoothedLevel, _currentLevel, smoothFall * Time.deltaTime);
                
                if (forceSmoothFall)
                {
                    _currentLevel = _smoothedLevel;
                }
                
                // Slowly return to the noise floor.
                _input = Mathf.Max(_input - decaySpeed * Time.deltaTime, 0);
                // Pull up by input with a small headroom.
                var room = dynamicRange * 0.05f;
                _input = Mathf.Clamp(_currentLevel + room, _input, 1);

                _clampedInput = Mathf.Clamp(_currentLevel, _input - dynamicRange, _input);

                // ((input - min) * 100) / (max - min)
                _normalizedInput = (_clampedInput - (_input - dynamicRange)) / (_input - (_input - dynamicRange));
            }
            
            smoothedMeanLevel = Mathf.Lerp(smoothedMeanLevel, _smoothedLevel, meanLevelSmoothSharpness * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            if (!debugDisplay)
                return;

            
        }
    }
}
