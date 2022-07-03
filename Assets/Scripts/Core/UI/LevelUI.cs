using Core.EventBus;
using Core.EventBus.Events;
using Core.Score;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI {
    public sealed class LevelUI : MonoBehaviour {
        [SerializeField] LivesView LivesView;
        [SerializeField] ScoreView ScoreView;
        [SerializeField] Button PauseButton;

        LivesController _livesController;
        ScoreController _scoreController;

        bool IsPaused => GameState.Instance.PauseManager.IsPaused;

        public void Init(LivesController livesController, ScoreController scoreController) {
            _livesController = livesController;
            _scoreController = scoreController;
            
            LivesView.Init(_livesController.Lives);
            ScoreView.Init(_scoreController.Score);
            
            PauseButton.onClick.AddListener(OnPauseButtonClicked);
            
            EventManager.Instance.Subscribe<LivesChanged>(this, OnLivesChanged);
            EventManager.Instance.Subscribe<ScoreChanged>(this, OnScoreChanged);
        }

        public void DeInit() {
            PauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
        }

        void OnPauseButtonClicked() {
            GameState.Instance.PauseManager.SetPaused(!IsPaused);
        }

        void OnLivesChanged(LivesChanged ev) {
            LivesView.UpdateLives(ev.Lives);
        }

        void OnScoreChanged(ScoreChanged ev) {
            ScoreView.UpdateScore(ev.Score);
        }

    }
}