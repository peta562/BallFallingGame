using Core.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WindowsUI.Windows {
    public sealed class EndOfLevelsWindow : BaseWindow {
        [SerializeField] Button OkButton;

        public override void Show() {
            OkButton.onClick.AddListener(OnOkButtonClicked);

            base.Show();
        }

        public override void Hide() {
            OkButton.onClick.RemoveListener(OnOkButtonClicked);
            
            base.Hide();
        }
        
        void OnOkButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            Hide();
        }
    }
}