using System;
using UnityEngine;

namespace TocaAssignment
{
    [Serializable]
    [CreateAssetMenu(menuName = "TocaAssignment/AudioEventListener")]
    public class AudioEventListener : ScriptableObject,IGameEventListener
    {
        public AudioSystem audioSystem;
        
        public void RegisterEvent()
        {
            _gameEvent.RegisterListener(this);
        }

        public void UnRegisterEvent()
        {
            _gameEvent.UnregisterListener(this);
        }
        public AudioPlayable audioPlayable;
        [SerializeField]
        private GameEvent _gameEvent;

        public void OnEventRaised()
        {
            audioSystem.Play(audioPlayable.GetAudioInfo());
        }

        public GameEvent gameEvent
        {
            get => _gameEvent;
            set => _gameEvent = value;
        }
    }
}