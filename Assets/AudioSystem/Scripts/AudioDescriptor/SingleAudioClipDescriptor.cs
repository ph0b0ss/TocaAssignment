using System.Collections.Generic;
using UnityEngine;

namespace TocaAssignment
{
    [CreateAssetMenu(menuName = "TocaAssignment/AudioPlayable/SinglePlayList")]
    public class SingleAudioClipDescriptor : AudioPlayable
    {
        public AudioInfo audioClip;
        

        public override AudioInfo GetAudioInfo()
        {
            return audioClip;
        }
    }
}