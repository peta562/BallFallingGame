using System;

namespace Core.SaveLoad {
    [Serializable]
    public sealed class SaveData {
        public int CurrentLevelId = 1;
        public float SoundVolume = 0.8f;
        public bool MusicOn = true;
        public bool SoundEffectsOn = true;
    }
}