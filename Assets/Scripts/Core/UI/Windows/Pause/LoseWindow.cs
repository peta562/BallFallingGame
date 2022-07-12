using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI.Windows.Pause {
    public sealed class LoseWindow : BaseWindow {
        [SerializeField] Button OkButton;

        public override void Show() {
            GameState.Instance.PauseManager.SetPaused(true);
            OkButton.onClick.AddListener(Hide);

            base.Show();
        }

        public override void Hide() {
            GameState.Instance.PauseManager.SetPaused(false);
            OkButton.onClick.RemoveListener(Hide);

            base.Hide();
            SceneManager.LoadScene("MainMenu");
        }
    }
}