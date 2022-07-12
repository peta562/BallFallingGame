using System.Collections.Generic;
using Configs;
using Core.EventBus;
using Core.Balls.BallEntity;
using Core.EventBus.Events;
using Core.Level;
using UnityEngine;

namespace Core.Balls {
    public sealed class BallsController : BaseController {
        readonly GameConfig _gameConfig;
        readonly BallConfig _ballConfig;
        readonly List<Ball> _balls = new List<Ball>();
        
        BallFactory _ballFactory;

        Vector2 _stageDimensions;
        float _ballSpawnDeltaTime;
        
        public BallsController(GameConfig gameConfig, BallConfig ballConfig) {
            _gameConfig = gameConfig;
            _ballConfig = ballConfig;
        }
        
        public override void Init() {
            
        }

        public void ChangeFactory(BallFactory ballFactory) {
            _ballFactory = ballFactory;
            _ballFactory.Init(_ballConfig);
        }

        public override void Update() {
            _ballSpawnDeltaTime += Time.deltaTime;
        
            _stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            
            CheckForSpawn();
            CheckBallsOutOfBounds();
            MoveBalls();
        }

        public override void DeInit() {
            
        }
        
        public void HandleBallClick(Ball ball) {
            ball.ApplyDamage(_gameConfig.Damage);
            
            if ( ball.Health <= 0 ) {
                ball.PlayDieEffect();
                RemoveBall(ball);
                
                EventManager.Instance.Fire(new BallKilled(ball));
            } else {
                ball.PlayHitEffect();
            }
        }

        public void HandleBallFall(Ball ball) {
            RemoveBall(ball);
        }

        public void FinishLevel() {
            for (var i = _balls.Count - 1; i >= 0; i--) {
                RemoveBall(_balls[i]);
            }
        }

        void CheckForSpawn() {
            if ( _ballSpawnDeltaTime < _gameConfig.BallDeltaSpawn ) {
                return;
            }

            SpawnBall();
            _ballSpawnDeltaTime = 0f;
        }

        void CheckBallsOutOfBounds() {
            for (var i = _balls.Count - 1; i >= 0; i--) {
                _balls[i].CheckOutOfBounds(_stageDimensions);
            }
        }

        void SpawnBall() {
            var ball = _ballFactory.GetBall(_stageDimensions);
            _balls.Add(ball);
        }

        void MoveBalls() {
            for (var i = _balls.Count - 1; i >= 0; i--) {
                _balls[i].Move(_gameConfig.Speed);
            }
        }

        void RemoveBall(Ball ball) {
            ball.DeInit();
            
            _ballFactory.RemoveBall(ball);
            _balls.Remove(ball);
        }
    }
}