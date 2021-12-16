using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class sMicrophone : UdonSharpBehaviour
{
    [Range(0f, 24f)]
    public float voiceGain;
    public float voiceFar;
    public float voiceNear;
    public bool voiceDisableLowpass;
    
    [Range(0f, 24f)]
    public float micVoiceGain;
    public float micVoiceFar;
    public float micVoiceNear;
    public bool micVoiceDisableLowpass;

    private VRCPlayerApi micOwner;

    public override void OnPickup()
    {
        SendCustomNetworkEvent(NetworkEventTarget.All, nameof(GrabbedMic));
    }

    public override void OnDrop()
    {
        SendCustomNetworkEvent(NetworkEventTarget.All, nameof(DroppedMic));
    }
    
    public void DroppedMic()
    {
        micOwner.SetVoiceGain(voiceGain);
        micOwner.SetVoiceDistanceFar(voiceFar);
        micOwner.SetVoiceDistanceNear(voiceNear);
        micOwner.SetVoiceLowpass(voiceDisableLowpass);
    }

    public void GrabbedMic()
    {
        micOwner = Networking.GetOwner(gameObject);
        micOwner.SetVoiceGain(micVoiceGain);
        micOwner.SetVoiceDistanceFar(micVoiceFar);
        micOwner.SetVoiceDistanceNear(micVoiceNear);
        micOwner.SetVoiceLowpass(micVoiceDisableLowpass);
    }
}
