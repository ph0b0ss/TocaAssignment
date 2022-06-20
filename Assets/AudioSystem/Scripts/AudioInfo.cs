using UnityEngine;
using UnityEngine.Audio;

namespace TocaAssignment
{
    [CreateAssetMenu(menuName = "TocaAssignment/AudioInfo")]
    public class AudioInfo : ScriptableObject
    {
        public AudioClip audioClip;
        public bool loop;
        public AudioMixerGroup audioMixerGroup;
    }
}