using UnityEngine;

namespace TocaAssignment
{
    public abstract class AudioPlayable : ScriptableObject
    {
        public abstract AudioInfo GetAudioInfo();
    }
}