using System;
using Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.PlayableObjects {
    public sealed class BonusController : PlayableObjectController {
        public BonusController(GameConfig gameConfig, PlayableObjectsConfig playableObjectsConfig) : base(gameConfig, playableObjectsConfig) {
        }
        
        public override void Init() {
        }

        public override void DeInit() {
        }
        
        public void FinishLevel() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                RemovePlayableObject(_playableObjects[i]);
            }
        }

        protected override void MovePlayableObjects() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                _playableObjects[i].Move(Vector3.down, _gameConfig.BonusSpeed * Time.deltaTime);
            }
        }

        protected override void CheckForSpawn() {
            if ( _playableObjectSpawnDeltaTime < _gameConfig.BonusDeltaSpawn ) {
                return;
            }

            SpawnBonus();
            _playableObjectSpawnDeltaTime = 0f;
        }

        void SpawnBonus() {
            var randomBonusType = (PlayableObjectType) Random.Range(1, Enum.GetValues(typeof(PlayableObjectType)).Length);
            
            var bonus = _playableObjectFactory.GetPlayableObject(randomBonusType, _stageDimensions);
            bonus.SetView();
            
            _playableObjects.Add(bonus);
        }
    }
}