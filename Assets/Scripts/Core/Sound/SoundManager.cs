using UnityEngine;

namespace Core.Sound {
    public sealed class SoundManager : Singleton<SoundManager> {
        [SerializeField] AudioClipHolder AudioClipHolder;
        [SerializeField] AudioSource SoundEffectsAudioSource;
        [SerializeField] AudioSource MusicAudioSource;

        public void PlaySound(AudioClipNames audioClipName) {
            var audioClip = GetAudioClip(audioClipName);

            if ( audioClip == null ) {
                Debug.LogError($"Can't find audio clip for {audioClipName.ToString()}");
                return;
            }

            if ( SoundEffectsAudioSource.isPlaying ) {
                SoundEffectsAudioSource.Stop();
            }
            
            SoundEffectsAudioSource.PlayOneShot(audioClip);
        }

        public void ChangeVolume(float value) {
            AudioListener.volume = value;
        }

        public void ToggleMusic(bool isOn) {
            MusicAudioSource.mute = !isOn;
        }
        
        public void ToggleSoundEffects(bool isOn) {
            SoundEffectsAudioSource.mute = !isOn;
        }

        AudioClip GetAudioClip(AudioClipNames audioClipName) {
            foreach (var sound in AudioClipHolder.Sounds) {
                if ( sound.AudioClipName == audioClipName ) {
                    return sound.AudioClip;
                }
            }

            return null;
        }
    }
}