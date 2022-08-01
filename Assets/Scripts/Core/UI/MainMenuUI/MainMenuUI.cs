using Core.Progress;
using Core.Sound;
using Core.UI.WindowsUI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.MainMenuUI {
    public sealed class MainMenuUI : MonoBehaviour {
        [SerializeField] Button PlayButton;
        [SerializeField] Button SettingsButton;

        ProgressController _progressController;

        public void Init(ProgressController progressController) {
            _progressController = progressController;
            
            PlayButton.onClick.AddListener(OnPlayButtonClicked);
            SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        public void DeInit() {
            PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
            SettingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }

        void OnPlayButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            _progressController.TryShowPlayWindow();
        }

        void OnSettingsButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            GameContext.Instance.WindowManager.ShowWindow<SettingsWindow>();
        }
    }
}