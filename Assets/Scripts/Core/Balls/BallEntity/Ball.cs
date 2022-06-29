using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;

namespace Core.Balls.BallEntity {
    public sealed class Ball : MonoBehaviour {
        [SerializeField] BallMover _ballMover;
        [SerializeField] BallView _ballView;
        [SerializeField] BallEffect _ballEffect;
        [SerializeField] BallFellDetector _ballFellDetector;
        [SerializeField] BallClickHandler _ballClickHandler;

        float _health;

        public float Health => _health;

        public void Init(float health, Vector2 position, float scale, Sprite sprite, Color color) {
            transform.position = position;

            _health = health;

            _ballView.Init(scale, sprite, color);
            _ballEffect.Init(color, scale);

            _ballFellDetector.OnBallFell += OnBallFell;
            _ballClickHandler.OnBallClicked += OnBallClick;
        }

        public void DeInit() {
            _ballFellDetector.OnBallFell -= OnBallFell;
            _ballClickHandler.OnBallClicked -= OnBallClick;
        }

        public void Move(float speed) {
            _ballMover.Move(speed);
        }

        public void CheckOutOfBounds(Vector2 stageDimensions) {
            _ballFellDetector.CheckOutOfBounds(transform.position, transform.localScale.y, stageDimensions);
        }

        public void PlayDieEffect() => _ballEffect.PlayDieEffect();
        
        public void PlayHitEffect() => _ballEffect.PlayHitEffect();

        public void ApplyDamage(float damage) {
            _health -= damage;
        }

        void OnBallFell() {
            EventManager.Instance.Fire(new BallFell(this));
        }
        
        void OnBallClick() {
            EventManager.Instance.Fire(new BallClicked(this));
        }
    }
}