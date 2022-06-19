using System;
using System.Collections;
using UnityEngine;

namespace TocaAssignment
{
    [RequireComponent(typeof(AudioSource))]
    public class TocaAudioSource : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        public event Action<TocaAudioSource> OnComplete; 

        private IEnumerator PlayingCheckRoutine()
        {
            while (audioSource.isPlaying)
            {
                yield return null;
            }
            
            OnComplete?.Invoke(this);
        }
        
        public void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }
        
        public void Play(AudioInfo info)
        {
            audioSource.clip = info.audioClip;
            audioSource.loop = info.loop;
            audioSource.outputAudioMixerGroup = info.audioMixerGroup;
            audioSource.Play();
            if (!info.loop)
            {
                StartCoroutine(PlayingCheckRoutine());
            }
        }

        
        
        
    }
}