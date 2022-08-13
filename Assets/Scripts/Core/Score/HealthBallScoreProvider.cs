using Core.EventBus;
using Core.EventBus.Events;

namespace Core.Score {
    public class HealthBallScoreProvider : IScoreProvider {
        int _health;

        public void Init() {
            EventManager.Instance.Subscribe<BallKilled>(this, OnBallKilled);
        }

        public void DeInit() {
            EventManager.Instance.Unsubscribe<BallKilled>(OnBallKilled);
        }
        
        public int GetScore() {
            return _health;
        }

        void OnBallKilled(BallKilled ev) {
            _health = ev.Ball.MaxHealth;
        }
    }
}