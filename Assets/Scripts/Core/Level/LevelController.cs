using Configs;
using Core.EventBus;
using Core.EventBus.Events;
using Core.Sound;
using Core.UI.WindowsUI;
using Core.UI.WindowsUI.Windows;

namespace Core.Level {
    public sealed class LevelController : BaseController {
        readonly WindowManager _windowManager;
        
        LevelInfo _levelInfo;

        public LevelController(GameContext gameContext) {
            _windowManager = gameContext.WindowManager;
        }
        
        public override void Init() {
        }

        public override void DeInit() {
        }
        
        public void StartLevel(LevelInfo levelInfo) {
            _levelInfo = levelInfo;
        }

        public void CheckForWin(int score) {
            if ( score >= _levelInfo.TargetScore ) {
                WinLevel();
            }
        }

        public void CheckForLose(int lives) {
            if ( lives <= 0 ) {
                LoseLevel();
            }
        }
        
        void WinLevel() {
            SoundManager.Instance.PlaySound(AudioClipNames.WinSound);
            EventManager.Instance.Fire(new LevelFinished(true));
            _windowManager.ShowWindow<WinWindow>();
        }

        void LoseLevel() {
            SoundManager.Instance.PlaySound(AudioClipNames.LoseSound);
            EventManager.Instance.Fire(new LevelFinished(false));
            _windowManager.ShowWindow<LoseWindow>();
        }
    }
}