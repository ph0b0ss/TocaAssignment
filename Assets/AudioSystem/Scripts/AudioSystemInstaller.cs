using System;
using System.Collections.Generic;
using UnityEngine;

namespace TocaAssignment
{
    public class AudioSystemInstaller : MonoBehaviour
    {
        public AudioSystem audioSystem;

        public List<AudioEventListener> AudioEventListeners = new List<AudioEventListener>();

        public void Awake()
        {
            foreach (var listener in AudioEventListeners)
            {
                listener.audioSystem = audioSystem;
            }
        }
    }
}