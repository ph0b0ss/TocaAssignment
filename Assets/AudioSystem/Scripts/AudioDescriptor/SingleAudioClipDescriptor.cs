using System.Collections.Generic;
using UnityEngine;

namespace TocaAssignment
{
    [CreateAssetMenu]
    public class SingleAudioClipDescriptor : AudioPlayable
    {
        public AudioInfo audioClip;
        

        public override AudioInfo GetAudioInfo()
        {
            return audioClip;
        }
    }
}