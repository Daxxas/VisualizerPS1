using UnityEngine;

namespace Events
{
    public class MusicEventAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private MusicEvent musicEvent;
        [SerializeField] private string triggerName;
        
        private void Start()
        {
            musicEvent.onEventStart.AddListener(StartAnimation);
        }

        public void StartAnimation()
        {
            animator.SetTrigger(triggerName);
        }
    }
}