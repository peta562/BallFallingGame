using Configs;
using Core.Level;
using Core.SaveLoad;
using Core.Score;
using Core.UI.WindowsUI;
using Core.UI.WindowsUI.Windows;

namespace Core.Progress {
    public sealed class ProgressController : BaseController {
        readonly ProgressConfig _progressConfig;
        readonly ScoreController _scoreController;
        readonly LevelController _levelController;
        readonly WindowManager _windowManager;
        
        int _currentLevelId;

        public ProgressController(ProgressConfig progressConfig, ScoreController scoreController,
            LevelController levelController, GameContext gameContext) {
            _progressConfig = progressConfig;
            _scoreController = scoreController;
            _levelController = levelController;
            _windowManager = gameContext.WindowManager;
        }

        public override void Init() {
        }

        public override void DeInit() {
        }

        public override void Save(SaveData saveData) {
            saveData.CurrentLevelId = _currentLevelId;
        }
        
        public override void Load(SaveData saveData) {
            _currentLevelId = saveData.CurrentLevelId;
        }
        
        public void TryShowPlayWindow() {
            LevelInfo currentLevelInfo = null;
            if ( _currentLevelId <= _progressConfig.MaxLevels ) {
                foreach (var level in _progressConfig.Levels) {
                    if ( _currentLevelId == level.Id ) {
                        currentLevelInfo = level;
                        break;
                    }
                }
            }

            if ( currentLevelInfo != null ) {
                _windowManager.ShowWindow<PlayWindow>(x =>
                    x.Init(_scoreController, _levelController, currentLevelInfo));
            } else {
                _windowManager.ShowWindow<EndOfLevelsWindow>();
            }
        }

        public void FinishLevel(bool win) {
            if ( win ) {
                _currentLevelId++;
            }
        }
    }    
}