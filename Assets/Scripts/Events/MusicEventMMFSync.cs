using MoreMountains.Feedbacks;
using UnityEngine;

namespace Events
{
    public class MusicEventMMFSync : MonoBehaviour
    {
        [SerializeField] private MusicEvent musicEvent;
        [SerializeField] private MMF_Player mmfPlayer;
        
        private void Awake()
        {
            //mmfPlayer.Revert();
            mmfPlayer.ShouldRevertOnNextPlay = true;
            musicEvent.onEventStop.AddListener(mmfPlayer.PlayFeedbacks);
        }
    }
}