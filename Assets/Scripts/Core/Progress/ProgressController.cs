using Configs;
using Core.EventBus;
using Core.EventBus.Events;
using Core.Level;
using Core.Score;
using Core.UI.Windows;
using Core.UI.Windows.Pause;

namespace Core.Progress {
    public sealed class ProgressController : BaseController {
        readonly ProgressConfig _progressConfig;
        readonly ScoreController _scoreController;
        readonly LevelController _levelController;
        readonly WindowManager _windowManager;
        
        int _currentLevelId;

        public ProgressController(ProgressConfig progressConfig, ScoreController scoreController,
            LevelController levelController, WindowManager windowManager) {
            _progressConfig = progressConfig;
            _scoreController = scoreController;
            _levelController = levelController;
            _windowManager = windowManager;
        }

        public override void Init() {
            EventManager.Instance.Subscribe<LevelLose>(this, OnLevelLose);
            EventManager.Instance.Subscribe<LevelWin>(this, OnLevelWin);
        }

        public override void DeInit() {
            EventManager.Instance.Unsubscribe<LevelLose>(OnLevelLose);
            EventManager.Instance.Unsubscribe<LevelWin>(OnLevelWin);
        }
        
        public void TryStartLevel() {
            if ( _currentLevelId <= _progressConfig.MaxLevels ) {
                LevelInfo currentLevelInfo = null;
                foreach (var level in _progressConfig.Levels) {
                    if ( _currentLevelId == level.Id ) {
                        currentLevelInfo = level;
                        break;
                    }
                }

                if ( currentLevelInfo != null ) {
                    _windowManager.ShowWindow<PlayWindow>(x => x.Init(_scoreController, _levelController, currentLevelInfo));
                } else {
                    _windowManager.ShowWindow<EndOfLevelsWindow>();  
                }

            }
        }

        void OnLevelLose(LevelLose ev) {
            
        }

        void OnLevelWin(LevelWin ev) {
            
            _currentLevelId++;
        }
    }    
}