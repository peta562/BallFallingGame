using System.Collections.Generic;
using Core.Balls;
using Core.Bonuses;
using Core.Bonuses.BonusEntity;
using Core.EventBus;
using Core.EventBus.Events;
using Core.Level;
using Core.Lives;
using Core.Progress;
using Core.Score;

namespace Core.GameManagers {
    public sealed class LevelManager {
        readonly ScoreController _scoreController;
        readonly LivesController _livesController;
        readonly ProgressController _progressController;
        readonly BallsController _ballsController;
        readonly BonusController _bonusController;
        readonly LevelController _levelController;

        readonly List<BaseController> _controllers = new List<BaseController>();
        
        bool IsPaused => GameContext.Instance.PauseManager.IsPaused;

        bool _isLevelActive;

        public LevelManager(ScoreController scoreController, LivesController livesController,
            ProgressController progressController, BallsController ballsController, BonusController bonusController, LevelController levelController) {
            _scoreController = scoreController;
            _livesController = livesController;
            _progressController = progressController;
            _ballsController = ballsController;
            _bonusController = bonusController;
            _levelController = levelController;
            
            _controllers.Add(scoreController);
            _controllers.Add(livesController);
            _controllers.Add(progressController);
            _controllers.Add(ballsController);
            _controllers.Add(bonusController);
            _controllers.Add(levelController);
        }

        public void Init() {
            _isLevelActive = true;
            
            foreach (var controller in _controllers) {
                controller.Init();
            }
            
            EventManager.Instance.Subscribe<LivesChanged>(this, OnLivesChanged);
            EventManager.Instance.Subscribe<ScoreChanged>(this, OnScoreChanged);
            EventManager.Instance.Subscribe<BallFell>(this, OnBallFell);
            EventManager.Instance.Subscribe<BallKilled>(this, OnBallKilled);
            EventManager.Instance.Subscribe<BallClicked>(this, OnBallClicked);
            EventManager.Instance.Subscribe<BonusFell>(this, OnBonusFell);
            EventManager.Instance.Subscribe<BonusKilled>(this, OnBonusKilled);
            EventManager.Instance.Subscribe<BonusClicked>(this, OnBonusClicked);
            EventManager.Instance.Subscribe<LevelFinished>(this, OnLevelFinished);
        }

        public void DeInit() {
            foreach (var controller in _controllers) {
                controller.DeInit();
            }
            
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
            EventManager.Instance.Unsubscribe<BallFell>(OnBallFell);
            EventManager.Instance.Unsubscribe<BallKilled>(OnBallKilled);
            EventManager.Instance.Unsubscribe<BallClicked>(OnBallClicked);
            EventManager.Instance.Unsubscribe<BonusFell>(OnBonusFell);
            EventManager.Instance.Unsubscribe<BonusKilled>(OnBonusKilled);
            EventManager.Instance.Unsubscribe<BonusClicked>(OnBonusClicked);
            EventManager.Instance.Unsubscribe<LevelFinished>(OnLevelFinished);
        }

        public void Update() {
            if ( IsPaused || !_isLevelActive) {
                return;
            }
            
            foreach (var controller in _controllers) {
                controller.Update();
            }
        }

        void OnLivesChanged(LivesChanged ev) {
            _levelController.CheckForLose(ev.Lives);
        }

        void OnScoreChanged(ScoreChanged ev) {
            _levelController.CheckForWin(ev.Score);
        }

        void OnBallFell(BallFell ev) {
            _ballsController.HandleBallFall(ev.Ball);
            _livesController.ReduceLive();
        }

        void OnBallKilled(BallKilled ev) {
            _scoreController.AddScore();
            _ballsController.HandleBallKill(ev.Ball);
        }

        void OnBallClicked(BallClicked ev) {
            _ballsController.HandleBallClick(ev.Ball);
        }
        
        void OnBonusFell(BonusFell ev) {
            _bonusController.HandleBonusFall(ev.Bonus);
        }

        void OnBonusKilled(BonusKilled ev) {
            switch (ev.Bonus.BonusType) {
                case BonusType.KillAllBalls:
                    var killAllBallsBonus = ev.Bonus as KillAllBallsBonus;
                    if ( killAllBallsBonus != null ) {
                        _ballsController.RemoveAllBalls();
                    }
                    break;
                case BonusType.AddLive:
                    var addLiveBonus = ev.Bonus as AddLiveBonus;
                    if ( addLiveBonus != null ) {
                        _livesController.AddLives(addLiveBonus.LivesToAdd);
                    }
                    break;
                case BonusType.MultiplyScore:
                    var multiplyScoreBonus = ev.Bonus as MultiplyScoreBonus;
                    if ( multiplyScoreBonus != null ) {
                        _scoreController.StartMultiplyScore(multiplyScoreBonus.Multiplier, multiplyScoreBonus.MultiplierTime);
                    }
                    break;
            }
            
            _bonusController.HandleBonusKill(ev.Bonus);
        }

        void OnBonusClicked(BonusClicked ev) {
            _bonusController.HandleBonusClick(ev.Bonus);
        }

        void OnLevelFinished(LevelFinished ev) {
            _isLevelActive = false;
            _progressController.FinishLevel(ev.Win);
            _ballsController.FinishLevel();
            _bonusController.FinishLevel();
        }
    }
}