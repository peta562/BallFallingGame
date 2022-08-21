using Core.SaveLoad;
using Core.Sound;

namespace Core.Settings {
    public sealed class SoundController : BaseController {
        public float SoundVolume { get; private set; }
        public bool MusicOn { get; private set; }
        public bool SoundEffectsOn { get; private set; }

        public override void Init() {
        }

        public override void DeInit() {
        }

        public override void Load(SaveData saveData) {
            SoundVolume = saveData.SoundVolume;
            MusicOn = saveData.MusicOn;
            SoundEffectsOn = saveData.SoundEffectsOn;
            
            SoundManager.Instance.ChangeVolume(SoundVolume);
            SoundManager.Instance.ToggleMusic(MusicOn);
            SoundManager.Instance.ToggleSoundEffects(SoundEffectsOn);
            
            SoundManager.Instance.StartPlayingMusic(AudioClipNames.MainMusic);
        }

        public override void Save(SaveData saveData) {
            saveData.SoundVolume = SoundVolume;
            saveData.MusicOn = MusicOn;
            saveData.SoundEffectsOn = SoundEffectsOn;
        }

        public void ChangeSoundVolume(float value) {
            SoundVolume = value;
            SoundManager.Instance.ChangeVolume(SoundVolume);
        }

        public void ToggleMusic() {
            MusicOn = !MusicOn;
            SoundManager.Instance.ToggleMusic(MusicOn);
        }

        public void ToggleSoundEffects() {
            SoundEffectsOn = !SoundEffectsOn;
            SoundManager.Instance.ToggleSoundEffects(SoundEffectsOn);
        }
    }
}