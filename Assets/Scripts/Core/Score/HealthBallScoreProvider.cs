using Core.EventBus;
using Core.EventBus.Events;

namespace Core.Score {
    public class HealthBallScoreProvider : IScoreProvider {
        int _health;

        public void Init() {
            EventManager.Instance.Subscribe<BallKilled>(this, OnBallSpawned);
        }

        public void DeInit() {
            EventManager.Instance.Unsubscribe<BallKilled>(OnBallSpawned);
        }
        
        public int GetScore() {
            return _health;
        }

        void OnBallSpawned(BallKilled ev) {
            _health = ev.Ball.MaxHealth;
        }
    }
}