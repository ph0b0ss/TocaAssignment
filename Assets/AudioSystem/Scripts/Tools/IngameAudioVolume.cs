using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IngameAudioVolume : MonoBehaviour
{
    public AudioMixerGroup MusicGroup;
    public void SetMusicVolume(float volume)
    {
        MusicGroup.audioMixer.SetFloat("Volume", volume);
    }
}
