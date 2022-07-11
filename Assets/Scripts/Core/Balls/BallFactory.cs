using Configs;
using Core.ObjectPool;
using Core.Balls.BallEntity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Balls {
    public sealed class BallFactory : MonoBehaviour {
        const int InitialStockCount = 5;

        BallConfig _ballConfig;
        ObjectPool<Ball> _objectPool;

        public void Init(BallConfig ballConfig) {
            _ballConfig = ballConfig;
            _objectPool = new ObjectPool<Ball>(BallFactoryMethod, TurnOnBall, TurnOffBall, InitialStockCount);
        }

        public Ball GetBall(Vector2 stageDimensions) {
            var ball = _objectPool.GetObject();

            var scale = _ballConfig.Scale.GetRandomFloat;
            var health = _ballConfig.Health.GetRandomFloat;
            var position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale), stageDimensions.y + scale);
            var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            
            ball.Init(health, position, scale, _ballConfig.Sprite, color);
            return ball;
        }

        public void RemoveBall(Ball ball) {
            _objectPool.ReturnObject(ball);
        }

        Ball BallFactoryMethod() => Instantiate(_ballConfig.Prefab);

        void TurnOnBall(Ball ball) => ball.gameObject.SetActive(true);
        
        void TurnOffBall(Ball ball) => ball.gameObject.SetActive(false);
    }
}