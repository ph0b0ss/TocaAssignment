using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace TocaAssignment
{
    public class AudioSystem : MonoBehaviour
    {
        
        public List<AudioEventListener> GameEventListeners = new List<AudioEventListener>();

        public TocaAudioSource audioSourcePrefab;

        public int POOLSIZE = 5;

        [SerializeField] private ObjectPool<TocaAudioSource> _audioSourcesPool;
        
        private TocaAudioSource _nextAudioSource
        {
            get
            {
                return _audioSourcesPool.Get();
            }
        }
        private void CreatePool()
        {
            _audioSourcesPool = new ObjectPool<TocaAudioSource>(createFunc: () => GameObject.Instantiate<TocaAudioSource>(audioSourcePrefab,transform), 
                actionOnGet: (obj) => obj.gameObject.SetActive(true), 
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj), 
                defaultCapacity: POOLSIZE);
        }

        private void InjectReferencesAndRegisterEvents()
        {
            foreach (var listener in GameEventListeners)
            {
                listener.audioSystem = this;
                listener.RegisterEvent();
            }
        }

        public void Awake()
        {
            InjectReferencesAndRegisterEvents();
        }

        public void OnDestroy()
        {
            foreach (var listener in GameEventListeners)
            {
                listener.UnRegisterEvent();
            }
        }

        public void Start()
        {
            CreatePool();
        }

        public void Play(AudioInfo audioInfo)
        {
            TocaAudioSource nextAudio = _nextAudioSource;
            nextAudio.Play(audioInfo);
            nextAudio.OnComplete += AudioOnOnComplete;
        }

        private void AudioOnOnComplete(TocaAudioSource audioSource)
        {
            audioSource.OnComplete -= AudioOnOnComplete;
            _audioSourcesPool.Release(audioSource);
        }
    }
}