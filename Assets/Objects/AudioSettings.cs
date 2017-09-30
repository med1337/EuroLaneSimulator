using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Audio Settings")]
public class AudioSettings : ScriptableObject
{
    public float music_volume = 0.5f;
    public float sfx_volume = 0.5f;

    public List<AudioClip> music_clips;
    public List<AudioClip> sfx_clips;

}
