using System.Collections.Generic;
using Core.Balls;
using Core.EventBus;
using Core.EventBus.Events;
using Core.Level;
using Core.Lives;
using Core.Progress;
using Core.Score;
using UnityEngine;

namespace Core.GameManagers {
    public sealed class LevelManager {
        readonly ScoreController _scoreController;
        readonly LivesController _livesController;
        readonly ProgressController _progressController;
        readonly BallsController _ballsController;
        readonly LevelController _levelController;

        readonly List<BaseController> _controllers = new List<BaseController>();
        
        bool IsPaused => GameState.Instance.PauseManager.IsPaused;

        public LevelManager(ScoreController scoreController, LivesController livesController,
            ProgressController progressController, BallsController ballsController, LevelController levelController) {
            _scoreController = scoreController;
            _livesController = livesController;
            _progressController = progressController;
            _ballsController = ballsController;
            _levelController = levelController;
            
            _controllers.Add(scoreController);
            _controllers.Add(livesController);
            _controllers.Add(progressController);
            _controllers.Add(ballsController);
            _controllers.Add(levelController);
        }

        public void Init() {
            foreach (var controller in _controllers) {
                controller.Init();
            }
            
            EventManager.Instance.Subscribe<LevelLose>(this, OnLevelLose);
            EventManager.Instance.Subscribe<LevelWin>(this, OnLevelWin);
            EventManager.Instance.Subscribe<LivesChanged>(this, OnLivesChanged);
            EventManager.Instance.Subscribe<ScoreChanged>(this, OnScoreChanged);
            EventManager.Instance.Subscribe<BallFell>(this, OnBallFell);
            EventManager.Instance.Subscribe<BallKilled>(this, OnBallKilled);
            EventManager.Instance.Subscribe<BallClicked>(this, OnBallClicked);
            EventManager.Instance.Subscribe<LevelFinished>(this, OnLevelFinished);
        }

        public void DeInit() {
            foreach (var controller in _controllers) {
                controller.DeInit();
            }
            
            EventManager.Instance.Unsubscribe<LevelLose>(OnLevelLose);
            EventManager.Instance.Unsubscribe<LevelWin>(OnLevelWin);
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
            EventManager.Instance.Unsubscribe<BallFell>(OnBallFell);
            EventManager.Instance.Unsubscribe<BallKilled>(OnBallKilled);
            EventManager.Instance.Unsubscribe<BallClicked>(OnBallClicked);
            EventManager.Instance.Unsubscribe<LevelFinished>(OnLevelFinished);
        }

        public void Update() {
            if ( IsPaused ) {
                return;
            }
            
            _ballsController.Update();
        }
        
        void OnLevelLose(LevelLose ev) {
            _progressController.LoseLevel();
        }

        void OnLevelWin(LevelWin ev) {
            _progressController.WinLevel();
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
        }

        void OnBallClicked(BallClicked ev) {
            _ballsController.HandleBallClick(ev.Ball);
        }

        void OnLevelFinished(LevelFinished ev) {
            _ballsController.FinishLevel();
        }
    }
}