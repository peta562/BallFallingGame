using Core.Scenes;
using Core.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WindowsUI.Windows {
    public sealed class LoseWindow : BaseWindow {
        [SerializeField] Button OkButton;

        public override void Show() {
            OkButton.onClick.AddListener(OnOkButtonClicked);

            base.Show();
        }

        public override void Hide() {
            OkButton.onClick.RemoveListener(OnOkButtonClicked);

            base.Hide();
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
        
        void OnOkButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            Hide();
        }
    }
}