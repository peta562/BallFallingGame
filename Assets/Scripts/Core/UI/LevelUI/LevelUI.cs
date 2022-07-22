using Core.EventBus;
using Core.EventBus.Events;
using Core.Lives;
using Core.Score;
using Core.UI.Windows;
using Core.UI.Windows.Pause;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.LevelUI {
    public sealed class LevelUI : MonoBehaviour {
        [SerializeField] LivesView LivesView;
        [SerializeField] ScoreView ScoreView;
        [SerializeField] Button PauseButton;

        LivesController _livesController;
        ScoreController _scoreController;
        WindowManager _windowManager;

        public void Init(LivesController livesController, ScoreController scoreController, WindowManager windowManager) {
            _livesController = livesController;
            _scoreController = scoreController;
            _windowManager = windowManager;

            LivesView.Init(_livesController.Lives);
            ScoreView.Init(_scoreController.Score);
            
            PauseButton.onClick.AddListener(OnPauseButtonClicked);
            
            EventManager.Instance.Subscribe<LivesChanged>(this, OnLivesChanged);
            EventManager.Instance.Subscribe<ScoreChanged>(this, OnScoreChanged);
            EventManager.Instance.Subscribe<ScoreMultiplyStarted>(this, OnScoreMultiplyStarted);
            EventManager.Instance.Subscribe<ScoreMultiplyStopped>(this, OnScoreMultiplyStopped);
        }

        public void DeInit() {
            _scoreController = null;
            _livesController = null;
            _windowManager = null;
            
            PauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
            EventManager.Instance.Unsubscribe<ScoreMultiplyStarted>(OnScoreMultiplyStarted);
            EventManager.Instance.Unsubscribe<ScoreMultiplyStopped>(OnScoreMultiplyStopped);
        }

        void OnPauseButtonClicked() {
            _windowManager.ShowWindow<PauseWindow>();
        }

        void OnLivesChanged(LivesChanged ev) {
            LivesView.UpdateLives(ev.Lives);
        }

        void OnScoreChanged(ScoreChanged ev) {
            ScoreView.UpdateScore(ev.Score);
        }

        void OnScoreMultiplyStarted(ScoreMultiplyStarted ev) {
            ScoreView.StartMultiplyScore(ev.Multiplier, ev.MultiplierTime);
        }

        void OnScoreMultiplyStopped(ScoreMultiplyStopped ev) {
            ScoreView.StopMultiplyScore();
        }
    }
}