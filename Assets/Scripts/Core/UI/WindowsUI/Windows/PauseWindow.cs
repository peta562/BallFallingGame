using Core.EventBus;
using Core.EventBus.Events;
using Core.Scenes;
using Core.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WindowsUI.Windows {
    public sealed class PauseWindow : BaseWindow {
        [SerializeField] Button ContinueButton;
        [SerializeField] Button ExitButton;

        public override void Show() {
            GameContext.Instance.PauseManager.SetPaused(true);
            ContinueButton.onClick.AddListener(Continue);
            ExitButton.onClick.AddListener(Exit);

            base.Show();
        }

        public override void Hide() {
            ContinueButton.onClick.RemoveListener(Continue);
            ExitButton.onClick.RemoveListener(Exit);
            
            base.Hide();
            GameContext.Instance.PauseManager.SetPaused(false);
        }

        void Continue() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            Hide();
        }
        
        void Exit() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            Hide();
            EventManager.Instance.Fire(new LevelFinished(false));
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
    }
}