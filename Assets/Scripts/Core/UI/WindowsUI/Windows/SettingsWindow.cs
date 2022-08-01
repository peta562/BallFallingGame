using Core.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WindowsUI.Windows {
    public sealed class SettingsWindow : BaseWindow {
        [SerializeField] Slider VolumeSlider;
        [SerializeField] Button ToggleMusicSoundButton;
        [SerializeField] Button ToggleEffectSoundButton;

        public override void Show() {
            VolumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
            ToggleMusicSoundButton.onClick.AddListener(OnToggleMusicSoundButtonClicked);
            ToggleEffectSoundButton.onClick.AddListener(OnToggleEffectSoundButtonClicked);
            
            base.Show();
        }

        public override void Hide() {
            VolumeSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
            ToggleMusicSoundButton.onClick.RemoveListener(OnToggleMusicSoundButtonClicked);
            ToggleEffectSoundButton.onClick.RemoveListener(OnToggleEffectSoundButtonClicked);
            
            base.Hide();
        }

        void OnSliderValueChanged(float value) {
            SoundManager.Instance.ChangeVolume(value);
        }

        void OnToggleMusicSoundButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            SoundManager.Instance.ToggleMusic();
        }

        void OnToggleEffectSoundButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            SoundManager.Instance.ToggleEffect();
        }
    }
}