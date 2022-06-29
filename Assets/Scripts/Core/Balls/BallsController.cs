using System.Collections.Generic;
using Configs;
using Core.EventBus;
using Core.Balls.BallEntity;
using Core.EventBus.Events;
using UnityEngine;

namespace Core.Balls {
    public sealed class BallsController : BaseController {
        readonly BallFactory _ballFactory;
        readonly GameConfig _gameConfig;

        List<Ball> _balls = new List<Ball>();

        Vector2 stageDimensions;
        float _ballSpawnDeltaTime;
        Camera _camera;

        public BallsController(GameConfig gameConfig, BallFactory ballFactory) {
            _gameConfig = gameConfig;
            _ballFactory = ballFactory;
        }
        
        public override void Init() {
            _camera = Camera.main;
            
            EventManager.Instance.Subscribe<BallClicked>(this, OnBallClicked);
            EventManager.Instance.Subscribe<BallFell>(this, OnBallFell);
        }

        public override void Update() {
            _ballSpawnDeltaTime += Time.deltaTime;
        
            stageDimensions = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            
            CheckForSpawn();
            CheckBallsOutOfBounds();
            MoveBalls();
        }

        public override void DeInit() {
            EventManager.Instance.Unsubscribe<BallClicked>(OnBallClicked);
            EventManager.Instance.Unsubscribe<BallFell>(OnBallFell);
        }

        void CheckForSpawn() {
            if ( _ballSpawnDeltaTime < _gameConfig.BallDeltaSpawn ) {
                return;
            }

            SpawnBall();
            _ballSpawnDeltaTime = 0f;
        }

        void CheckBallsOutOfBounds() {
            for (int i = _balls.Count - 1; i >= 0; i--) {
                _balls[i].CheckOutOfBounds(stageDimensions);
            }
        }

        void SpawnBall() {
            var ball = _ballFactory.GetBall(stageDimensions);
            _balls.Add(ball);
        }

        void MoveBalls() {
            for (int i = _balls.Count - 1; i >= 0; i--) {
                _balls[i].Move(_gameConfig.Speed);
            }
        }

        void OnBallClicked(BallClicked ev) {
            var ball = ev.Ball;
            
            ball.ApplyDamage(_gameConfig.Damage);
            
            if ( ball.Health <= 0 ) {
                ball.PlayDieEffect();
                RemoveBall(ball);
                
                EventManager.Instance.Fire(new BallKilled(ball));
            } else {
                ball.PlayHitEffect();
            }
        }

        void OnBallFell(BallFell ev) {
            RemoveBall(ev.Ball);
        }

        void RemoveBall(Ball ball) {
            ball.DeInit();
            
            _ballFactory.RemoveBall(ball);
            _balls.Remove(ball);
        }
    }
}