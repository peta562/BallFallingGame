using Configs;
using Core.EventBus;
using Core.EventBus.Events;
using Core.UI.Windows;
using Core.UI.Windows.Pause;

namespace Core.Level {
    public sealed class LevelController : BaseController {
        readonly WindowManager _windowManager;
        
        LevelInfo _levelInfo;

        public LevelController(WindowManager windowManager) {
            _windowManager = windowManager;
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
            EventManager.Instance.Fire(new LevelWin());
            FinishLevel();
            _windowManager.ShowWindow<WinWindow>();
        }

        void LoseLevel() {
            EventManager.Instance.Fire(new LevelLose());
            FinishLevel();
            _windowManager.ShowWindow<LoseWindow>();
        }

        void FinishLevel() {
            EventManager.Instance.Fire(new LevelFinished());
        }
    }
}