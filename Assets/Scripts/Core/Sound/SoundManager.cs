using UnityEngine;

namespace Core.Sound {
    public sealed class SoundManager : Singleton<SoundManager> {
        [SerializeField] AudioClipHolder AudioClipHolder;
        [SerializeField] AudioSource EffectAudioSource;
        [SerializeField] AudioSource MusicAudioSource;

        public void PlaySound(AudioClipNames audioClipName) {
            var audioClip = GetAudioClip(audioClipName);

            if ( audioClip == null ) {
                Debug.LogError($"Can't find audio clip for {audioClipName.ToString()}");
                return;
            }

            if ( EffectAudioSource.isPlaying ) {
                EffectAudioSource.Stop();
            }
            
            EffectAudioSource.PlayOneShot(audioClip);
        }

        public void ChangeVolume(float value) {
            AudioListener.volume = value;
        }

        public void ToggleEffect() {
            EffectAudioSource.mute = !EffectAudioSource.mute;
        }

        public void ToggleMusic() {
            MusicAudioSource.mute = !MusicAudioSource.mute;
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