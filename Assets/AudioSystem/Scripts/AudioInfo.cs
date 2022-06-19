using UnityEngine;
using UnityEngine.Audio;

namespace TocaAssignment
{
    [CreateAssetMenu]
    public class AudioInfo : ScriptableObject
    {
        public AudioClip audioClip;
        public bool loop;
        public AudioMixerGroup audioMixerGroup;
    }
}