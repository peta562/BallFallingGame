using System;
using System.Collections.Generic;
using Configs;
using Core.Bonuses.BonusEntity;
using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Bonuses {
    public sealed class BonusController : BaseController {
        readonly GameConfig _gameConfig;
        readonly BonusConfig _bonusConfig;
        
        readonly List<Bonus> _bonuses = new List<Bonus>();
        
        BonusFactory _bonusFactory;
        Vector2 _stageDimensions;
        float _bonusSpawnDeltaTime;

        public BonusController(GameConfig gameConfig, BonusConfig bonusConfig) {
            _gameConfig = gameConfig;
            _bonusConfig = bonusConfig;
        }
        
        public void ChangeFactory(BonusFactory bonusFactory) {
            _bonusFactory = bonusFactory;
            _bonusFactory.Init(_bonusConfig);
        }
        
        public override void Init() {
        }
        
        public override void DeInit() {
        }

        public override void Update() {
            _bonusSpawnDeltaTime += Time.deltaTime;
        
            _stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            
            CheckForSpawn();
            CheckBonusesOutOfBounds();
            MoveBonuses();
        }
        
        public void HandleBonusClick(Bonus bonus) {
            bonus.TakeDamage(_gameConfig.Damage);
            
            if ( bonus.Health <= 0 ) {
                EventManager.Instance.Fire(new BonusKilled(bonus));
            } else {
                bonus.PlayHitEffect();
            }
        }

        public void HandleBonusKill(Bonus bonus) {
            bonus.PlayDieEffect();
            RemoveBonus(bonus);
        }

        public void HandleBonusFall(Bonus bonus) {
            RemoveBonus(bonus);
        }
        
        public void FinishLevel() {
            for (var i = _bonuses.Count - 1; i >= 0; i--) {
                RemoveBonus(_bonuses[i]);
            }
        }

        void CheckForSpawn() {
            if ( _bonusSpawnDeltaTime < _gameConfig.BonusDeltaSpawn ) {
                return;
            }

            SpawnBonus();
            _bonusSpawnDeltaTime = 0f;
        }

        void CheckBonusesOutOfBounds() {
            for (var i = _bonuses.Count - 1; i >= 0; i--) {
                _bonuses[i].CheckOutOfBounds(_stageDimensions);
            }
        }

        void SpawnBonus() {
            var randomBonusType = (BonusType) Random.Range(0, Enum.GetValues(typeof(BonusType)).Length);
            
            var bonus = _bonusFactory.GetBonus(randomBonusType, _stageDimensions);
            bonus.SetView();
            
            _bonuses.Add(bonus);
        }

        void MoveBonuses() {
            for (var i = _bonuses.Count - 1; i >= 0; i--) {
                _bonuses[i].Move(Vector3.down,_gameConfig.BonusSpeed * Time.deltaTime);
            }
        }
        
        void RemoveBonus(Bonus bonus) {
            bonus.DeInit();
            
            _bonusFactory.RemoveBonus(bonus);
            _bonuses.Remove(bonus);
        }
    }
}