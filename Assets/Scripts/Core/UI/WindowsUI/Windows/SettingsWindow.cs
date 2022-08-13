using Core.Settings;
using Core.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WindowsUI.Windows {
    public sealed class SettingsWindow : BaseWindow {
        [SerializeField] Slider VolumeSlider;
        [SerializeField] Button ToggleMusicSoundButton;
        [SerializeField] Button ToggleEffectSoundButton;

        [Space]
        [SerializeField] Image MusicSoundButtonImage;
        [SerializeField] Image EffectsSoundButtonImage;
        
        [Space]
        [SerializeField] Sprite MusicSoundButtonOnSprite;
        [SerializeField] Sprite MusicSoundButtonOffSprite;

        [Space] 
        [SerializeField] Sprite EffectsSoundButtonOnSprite;
        [SerializeField] Sprite EffectsSoundButtonOffSprite;

        SettingsController _settingsController;
        
        public void Init(SettingsController settingsController) {
            _settingsController = settingsController;
        }
        
        public override void Show() {
            VolumeSlider.value = _settingsController.SoundVolume;
            ChangeMusicButtonSprite(_settingsController.MusicOn);
            ChangeSoundEffectsButtonSprite(_settingsController.SoundEffectsOn);

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
            _settingsController.ChangeSoundVolume(value);
        }

        void OnToggleMusicSoundButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            
            _settingsController.ToggleMusic();
            ChangeMusicButtonSprite(_settingsController.MusicOn);
        }

        void OnToggleEffectSoundButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            
            _settingsController.ToggleSoundEffects();
            ChangeSoundEffectsButtonSprite(_settingsController.SoundEffectsOn);
        }

        void ChangeMusicButtonSprite(bool isOn) {
            MusicSoundButtonImage.sprite =
                isOn ? MusicSoundButtonOnSprite : MusicSoundButtonOffSprite;
        }

        void ChangeSoundEffectsButtonSprite(bool isOn) {
            EffectsSoundButtonImage.sprite = isOn
                ? EffectsSoundButtonOnSprite
                : EffectsSoundButtonOffSprite;
        }
    }
}