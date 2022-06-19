using System.Collections.Generic;
using TocaAssignment;
using UnityEngine;

namespace TocaAssignment
{

    [CreateAssetMenu]
    public class RandomAudioPlayList : AudioPlayable
    {
        public List<AudioInfo> audioClips = new List<AudioInfo>();
       
        public override AudioInfo GetAudioInfo()
        {
            return audioClips[Random.Range(0, audioClips.Count)];
        }
    }
}