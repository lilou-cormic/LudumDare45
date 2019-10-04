using PurpleCable;
using UnityEngine;

public static class AudioClipExtensions
{
    public static void Play(this AudioClip audioClip)
    {
        SoundPlayer.Play(audioClip);
    }
}
