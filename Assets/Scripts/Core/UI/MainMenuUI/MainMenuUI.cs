using Core.Progress;
using Core.Settings;
using Core.Sound;
using Core.UI.WindowsUI;
using Core.UI.WindowsUI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.MainMenuUI {
    public sealed class MainMenuUI : MonoBehaviour {
        [SerializeField] Button PlayButton;
        [SerializeField] Button SettingsButton;

        ProgressController _progressController;
        SettingsController _settingsController;
        WindowManager _windowManager;

        public void Init(ProgressController progressController, SettingsController settingsController,
            WindowManager windowManager) {
            _progressController = progressController;
            _settingsController = settingsController;
            _windowManager = windowManager;

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
            _windowManager.ShowWindow<SettingsWindow>(x => x.Init(_settingsController));
        }
    }
}