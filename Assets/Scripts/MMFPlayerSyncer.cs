using System;
using MoreMountains.Feedbacks;
using UnityEngine;

[RequireComponent(typeof(MMF_Player))]
public class MMFPlayerSyncer : AudioSyncer
{
    private MMF_Player player;

    private void Start()
    {
        player = GetComponent<MMF_Player>();
    }

    public override void OnBeat()
    {
        base.OnBeat();
        player.PlayFeedbacks();
    }
}
