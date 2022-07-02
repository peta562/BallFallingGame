using Core.EventBus;
using Core.EventBus.Events;
using Core.Score;
using UnityEngine;

namespace Core.UI {
    public sealed class LevelUI : MonoBehaviour {
        [SerializeField] LivesView LivesView;
        [SerializeField] ScoreView ScoreView;

        LivesController _livesController;
        ScoreController _scoreController;

        public void Init(LivesController livesController, ScoreController scoreController) {
            _livesController = livesController;
            _scoreController = scoreController;
            
            LivesView.Init(_livesController.Lives);
            ScoreView.Init(_scoreController.Score);
            
            EventManager.Instance.Subscribe<LivesChanged>(this, OnLivesChanged);
            EventManager.Instance.Subscribe<ScoreChanged>(this, OnScoreChanged);
        }

        public void DeInit() {
            EventManager.Instance.Unsubscribe<LivesChanged>(OnLivesChanged);
            EventManager.Instance.Unsubscribe<ScoreChanged>(OnScoreChanged);
        }

        void OnLivesChanged(LivesChanged ev) {
            LivesView.UpdateLives(ev.Lives);
        }

        void OnScoreChanged(ScoreChanged ev) {
            ScoreView.UpdateScore(ev.Score);
        }

    }
}