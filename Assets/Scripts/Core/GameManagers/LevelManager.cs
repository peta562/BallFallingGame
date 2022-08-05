using System;
using System.Collections.Generic;
using Core.EventBus;
using Core.EventBus.Events;
using Core.Level;
using Core.Lives;
using Core.PlayableObjects;
using Core.PlayableObjects.Balls;
using Core.PlayableObjects.Bonuses;
using Core.Progress;
using Core.Score;
using Core.Sound;

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
            ProgressController progressController, BallsController ballsController,
            BonusController bonusController, LevelController levelController) {
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
            EventManager.Instance.Subscribe<PlayableObjectFell>(this, OnPlayableObjectFell);
            EventManager.Instance.Subscribe<PlayableObjectKilled>(this, OnPlayableObjectKilled);
            EventManager.Instance.Subscribe<PlayableObjectClicked>(this, OnPlayableObjectClicked);
            EventManager.Instance.Subscribe<LevelFinished>(this, OnLevelFinished);
        }

        public void DeInit() {
            foreach (var controller in _controllers) {
                controller.DeInit();
            }
            
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
            EventManager.Instance.Unsubscribe<PlayableObjectFell>(OnPlayableObjectFell);
            EventManager.Instance.Unsubscribe<PlayableObjectKilled>(OnPlayableObjectKilled);
            EventManager.Instance.Unsubscribe<PlayableObjectClicked>(OnPlayableObjectClicked);
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

        void OnPlayableObjectFell(PlayableObjectFell ev) {
            switch (ev.PlayableObject.PlayableObjectType) {
                case PlayableObjectType.Ball:
                    _ballsController.HandlePlayableObjectFall(ev.PlayableObject);
                    _livesController.ReduceLive();
                    break;
                case PlayableObjectType.AddLiveBonus:
                case PlayableObjectType.MultiplyScoreBonus:
                case PlayableObjectType.KillAllBallsBonus:
                    _bonusController.HandlePlayableObjectFall(ev.PlayableObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"There are no PlayableObject to handle fall for {ev.PlayableObject.PlayableObjectType} type");
            }
        }

        void OnPlayableObjectKilled(PlayableObjectKilled ev) {
            SoundManager.Instance.PlaySound(AudioClipNames.ObjectKill);
            
            switch (ev.PlayableObject.PlayableObjectType) {
                case PlayableObjectType.Ball:
                    _ballsController.HandlePlayableObjectKill(ev.PlayableObject);
                    EventManager.Instance.Fire(new BallKilled(ev.PlayableObject as Ball));
                    _scoreController.AddScore();
                    break;
                case PlayableObjectType.KillAllBallsBonus:
                    _ballsController.KillAllBalls();
                    _bonusController.HandlePlayableObjectKill(ev.PlayableObject);
                    break;
                case PlayableObjectType.AddLiveBonus:
                    var addLiveBonus = ev.PlayableObject as AddLiveBonus;
                    if ( addLiveBonus != null ) {
                        _livesController.AddLives(addLiveBonus.LivesToAdd);
                    }
                    _bonusController.HandlePlayableObjectKill(ev.PlayableObject);
                    break;
                case PlayableObjectType.MultiplyScoreBonus:
                    var multiplyScoreBonus = ev.PlayableObject as MultiplyScoreBonus;
                    if ( multiplyScoreBonus != null ) {
                        _scoreController.StartMultiplyScore(multiplyScoreBonus.Multiplier, multiplyScoreBonus.MultiplierTime);
                    }
                    _bonusController.HandlePlayableObjectKill(ev.PlayableObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"There are no PlayableObject to kill for {ev.PlayableObject.PlayableObjectType} type");
            }
        }

        void OnPlayableObjectClicked(PlayableObjectClicked ev) {
            SoundManager.Instance.PlaySound(AudioClipNames.ObjectClick);
            
            switch (ev.PlayableObject.PlayableObjectType) {
                case PlayableObjectType.Ball:
                    _ballsController.HandlePlayableObjectClick(ev.PlayableObject);
                    break;
                case PlayableObjectType.AddLiveBonus:
                case PlayableObjectType.KillAllBallsBonus:
                case PlayableObjectType.MultiplyScoreBonus:
                    _bonusController.HandlePlayableObjectClick(ev.PlayableObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"There are no PlayableObject to handle click for {ev.PlayableObject.PlayableObjectType} type");
            }
        }

        void OnLevelFinished(LevelFinished ev) {
            _isLevelActive = false;
            _progressController.FinishLevel(ev.Win);
            _ballsController.FinishLevel();
            _bonusController.FinishLevel();
        }
    }
}