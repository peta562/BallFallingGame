using Core.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI.Windows.Pause {
    public sealed class WinWindow : BaseWindow {
        [SerializeField] Button OkButton;

        public override void Show() {
            GameState.Instance.PauseManager.SetPaused(true);
            OkButton.onClick.AddListener(Hide);

            base.Show();
        }

        public override void Hide() {
            OkButton.onClick.RemoveListener(Hide);

            base.Hide();
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
            GameState.Instance.PauseManager.SetPaused(false);
        }
    }
}