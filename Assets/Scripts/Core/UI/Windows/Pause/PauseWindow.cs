using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Windows.Pause {
    public sealed class PauseWindow : BaseWindow {
        [SerializeField] Button ContinueButton;

        public override void Show() {
            GameState.Instance.PauseManager.SetPaused(true);
            ContinueButton.onClick.AddListener(Hide);

            base.Show();
        }

        public override void Hide() {
            GameState.Instance.PauseManager.SetPaused(false);
            ContinueButton.onClick.RemoveListener(Hide);
            
            base.Hide();
        }
    }
}