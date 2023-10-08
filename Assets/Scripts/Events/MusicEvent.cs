using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class MusicEvent : MonoBehaviour
    {
        public UnityEvent onEventStart;
        public UnityEvent onEventStop;
        [SerializeField] private Transform graphics;
        [SerializeField] private float eventStopDelay = 0f;
        [SerializeField] private float duration = 30f;
        [SerializeField] private List<GameObject> additionalGraphics;
        

        private bool isPlaying = false;
        private float timePlayed = 0f;
        
        [ContextMenu("Start Event")]
        public void StartEvent()
        {
            isPlaying = true;
            graphics.gameObject.SetActive(true);
            SetActiveAdditionalGraphics(true);
            onEventStart.Invoke();
        }

        public void StopEvent()
        {
            isPlaying = false;
            timePlayed = 0f;
            onEventStop?.Invoke();
            StartCoroutine(DelayedStop());
        }
        
        private void Update()
        {
            if (!isPlaying)
                return;
            
            timePlayed += Time.deltaTime;
            if (timePlayed > duration)
            {
                StopEvent();
            }
        }
        
        private IEnumerator DelayedStop()
        {
            yield return new WaitForSeconds(eventStopDelay);
            graphics.gameObject.SetActive(false);
            SetActiveAdditionalGraphics(false);
        }
        
        private void SetActiveAdditionalGraphics(bool active)
        {
            foreach (var additionalGraphic in additionalGraphics)
            {
                additionalGraphic.SetActive(active);
            }
        }
    }
}