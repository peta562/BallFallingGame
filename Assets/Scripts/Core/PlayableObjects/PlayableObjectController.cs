using System;
using System.Collections.Generic;
using Configs;
using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.PlayableObjects {
    public abstract class PlayableObjectController : BaseController {
        protected readonly GameConfig _gameConfig;
        protected readonly List<PlayableObject> _playableObjects = new List<PlayableObject>();
        
        protected PlayableObjectFactory _playableObjectFactory;
        protected Vector2 _stageDimensions;
        protected float _playableObjectSpawnDeltaTime;
        
        readonly PlayableObjectsConfig _playableObjectsConfig;

        protected PlayableObjectController(GameConfig gameConfig, PlayableObjectsConfig playableObjectsConfig) {
            _gameConfig = gameConfig;
            _playableObjectsConfig = playableObjectsConfig;
        }

        protected abstract void CheckForSpawn();
        
        protected abstract void MovePlayableObjects();
        
        public void SetFactory(PlayableObjectFactory playableObjectFactory) {
            _playableObjectFactory = playableObjectFactory;
            _playableObjectFactory.Init(_playableObjectsConfig);
        }

        public override void Init() {
        }
        
        public override void DeInit() {
        }

        public override void Update() {
            _playableObjectSpawnDeltaTime += Time.deltaTime;
        
            _stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            
            CheckForSpawn();
            CheckPlayableObjectsOutOfBounds();
            MovePlayableObjects();
        }
        
        public void HandlePlayableObjectClick(PlayableObject playableObject) {
            playableObject.TakeDamage(_gameConfig.Damage);
            
            if ( playableObject.Health <= 0 ) {
                EventManager.Instance.Fire(new PlayableObjectKilled(playableObject));
            } else {
                playableObject.PlayHitEffect();
            }
        }

        public void HandlePlayableObjectKill(PlayableObject playableObject) {
            playableObject.PlayDieEffect();
            RemovePlayableObject(playableObject);
        }

        public void HandlePlayableObjectFall(PlayableObject playableObject) {
            RemovePlayableObject(playableObject);
        }

        protected void RemovePlayableObject(PlayableObject playableObject) {
            playableObject.DeInit();

            _playableObjectFactory.RemovePlayableObject(playableObject);
            _playableObjects.Remove(playableObject);
        }
        
        void CheckPlayableObjectsOutOfBounds() {
            for (var i = _playableObjects.Count - 1; i >= 0; i--) {
                _playableObjects[i].CheckOutOfBounds(_stageDimensions);
            }
        }
    }
}