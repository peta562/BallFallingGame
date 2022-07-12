using System.Linq.Expressions;
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
            EventManager.Instance.Subscribe<LivesChanged>(this, OnLivesChanged);
            EventManager.Instance.Subscribe<ScoreChanged>(this, OnScoreChanged);
        }

        public override void DeInit() {
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
        }
        
        public void StartLevel(LevelInfo levelInfo) {
            _levelInfo = levelInfo;
        }

        void OnScoreChanged(ScoreChanged ev) {
            if ( ev.Score >= _levelInfo.TargetScore ) {
                WinLevel();
            }
        }

        void OnLivesChanged(LivesChanged ev) {
            if ( ev.Lives <= 0 ) {
                LoseLevel();
            }
        }
        
        void WinLevel() {
            FinishLevel();
            _windowManager.ShowWindow<WinWindow>();
        }

        void LoseLevel() {
            FinishLevel();
            _windowManager.ShowWindow<LoseWindow>();
        }

        void FinishLevel() {
            EventManager.Instance.Fire(new LevelFinished());
        }
    }
}