using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Sound {
    [Serializable]
    public class Sound {
        public AudioClipNames AudioClipName;
        public AudioClip AudioClip;
    }
    public sealed class AudioClipHolder : MonoBehaviour {
        public List<Sound> Sounds = new List<Sound>();
    }
}