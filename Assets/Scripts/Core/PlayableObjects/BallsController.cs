using Configs;
using Core.EventBus;
using Core.EventBus.Events;
using Core.PlayableObjects.Balls;
using UnityEngine;

namespace Core.PlayableObjects {
    public sealed class BallsController : PlayableObjectController {
        public BallsController(GameConfig gameConfig, PlayableObjectsConfig playableObjectsConfig) : base(gameConfig, playableObjectsConfig)  {
        }
        
        public override void Init() {
            
        }
        
        public override void DeInit() {
            
        }

        public void KillAllBalls() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                EventManager.Instance.Fire(new PlayableObjectKilled(_playableObjects[i]));
            }
        }
        
        public void FinishLevel() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                RemovePlayableObject(_playableObjects[i]);
            }
        }
        
        protected override void MovePlayableObjects() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                _playableObjects[i].Move(Vector3.down, _gameConfig.BallSpeed * Time.deltaTime);
            }
        }
        
        protected override void CheckPlayableObjectsOutOfBounds() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                if (_playableObjects[i].CheckOutOfBounds(_stageDimensions)) {
                    EventManager.Instance.Fire(new PlayableObjectFell(_playableObjects[i]));
                }
            }
        }

        protected override void CheckForSpawn() {
            if ( _playableObjectSpawnDeltaTime < _gameConfig.BallDeltaSpawn ) {
                return;
            }

            SpawnBall();
            _playableObjectSpawnDeltaTime = 0f;
        }

        void SpawnBall() {
            var ball = _playableObjectFactory.GetPlayableObject(PlayableObjectType.Ball, _stageDimensions);
            ball.SetView();
            
            _playableObjects.Add(ball);
        }
    }
}