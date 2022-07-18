using Core.EventBus;
using Core.EventBus.Events;
using Core.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Windows.Pause {
    public sealed class PauseWindow : BaseWindow {
        [SerializeField] Button ContinueButton;
        [SerializeField] Button ExitButton;

        public override void Show() {
            GameContext.Instance.PauseManager.SetPaused(true);
            ContinueButton.onClick.AddListener(Hide);
            ExitButton.onClick.AddListener(Exit);

            base.Show();
        }

        public override void Hide() {
            ContinueButton.onClick.RemoveListener(Hide);
            ExitButton.onClick.RemoveListener(Exit);
            
            base.Hide();
            GameContext.Instance.PauseManager.SetPaused(false);
        }

        void Exit() {
            Hide();
            EventManager.Instance.Fire(new LevelFinished(false));
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
    }
}