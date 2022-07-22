using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;

namespace Core.Balls.BallEntity {
    public sealed class Ball : MonoBehaviour {
        [SerializeField] BallMover BallMover;
        [SerializeField] BallView BallView;
        [SerializeField] BallEffect BallEffect;
        [SerializeField] BallFellDetector BallFellDetector;
        [SerializeField] ClickHandler clickHandler;

        float _health;

        public float Health => _health;

        public void Init(float health, Vector2 position, float scale, Sprite sprite, Color color) {
            transform.position = position;
            transform.localScale = new Vector2(scale, scale);

            _health = health;

            BallView.Init(sprite, color);
            BallEffect.Init(color, scale);
            BallFellDetector.Init(OnBallFell);
            
            clickHandler.OnClick += OnBallClick;
        }

        public void DeInit() {
            BallFellDetector.DeInit();
            
            clickHandler.OnClick -= OnBallClick;
        }

        public void Move(float speed) {
            BallMover.Move(speed);
        }

        public void CheckOutOfBounds(Vector2 stageDimensions) {
            BallFellDetector.CheckOutOfBounds(transform.position, transform.localScale.y, stageDimensions);
        }

        public void PlayDieEffect() => BallEffect.PlayDieEffect();
        
        public void PlayHitEffect() => BallEffect.PlayHitEffect();

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