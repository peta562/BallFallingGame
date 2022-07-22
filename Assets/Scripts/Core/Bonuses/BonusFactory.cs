using System;
using Configs;
using Core.Bonuses.BonusEntity;
using Core.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Bonuses {
    public sealed class BonusFactory : MonoBehaviour {
        const int InitialStockCount = 5;

        BonusConfig _bonusConfig;
        
        ObjectPool<KillAllBallsBonus> _objectPoolKillAllBallsBonus;
        ObjectPool<AddLiveBonus> _objectPoolAddLiveBonus;
        ObjectPool<MultiplyScoreBonus> _objectPoolMultiplyScoreBonus;

        public void Init(BonusConfig bonusConfig) {
            _bonusConfig = bonusConfig;
            
            _objectPoolKillAllBallsBonus = new ObjectPool<KillAllBallsBonus>(KillAllBallsBonusFactoryMethod, TurnOnBonus, TurnOffBonus, InitialStockCount);
            _objectPoolAddLiveBonus = new ObjectPool<AddLiveBonus>(AddLiveBonusFactoryMethod, TurnOnBonus, TurnOffBonus, InitialStockCount);
            _objectPoolMultiplyScoreBonus = new ObjectPool<MultiplyScoreBonus>(MultiplyScoreBonusFactoryMethod, TurnOnBonus, TurnOffBonus, InitialStockCount);
        }

        public Bonus GetBonus(BonusType bonusType, Vector2 stageDimensions) {
            float scale;
            Vector2 position;

            switch (bonusType) {
                case BonusType.KillAllBalls:
                    var killAllBallsBonus = _objectPoolKillAllBallsBonus.GetObject();

                    var killAllBallsBonusConfig = _bonusConfig.KillAllBallsBonusDescription;
                    scale = killAllBallsBonusConfig.Scale;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);

                    killAllBallsBonus.Init(bonusType, killAllBallsBonusConfig.Health, position, scale,
                        killAllBallsBonusConfig.Sprite);

                    return killAllBallsBonus;
                case BonusType.AddLive:
                    var addLiveBonus = _objectPoolAddLiveBonus.GetObject();

                    var addLiveBonusConfig = _bonusConfig.AddLiveBonusDescription;
                    scale = addLiveBonusConfig.Scale;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);

                    addLiveBonus.Init(bonusType, addLiveBonusConfig.Health, position, scale, addLiveBonusConfig.Sprite,
                        addLiveBonusConfig.LiveToAdd);

                    return addLiveBonus;
                case BonusType.MultiplyScore:
                    var multiplyScoreBonus = _objectPoolMultiplyScoreBonus.GetObject();

                    var multiplyScoreBonusConfig = _bonusConfig.MultiplyScoreBonusDescription;
                    scale = multiplyScoreBonusConfig.Scale;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);

                    multiplyScoreBonus.Init(bonusType, multiplyScoreBonusConfig.Health, position, scale,
                        multiplyScoreBonusConfig.Sprite, multiplyScoreBonusConfig.Multiplier,
                        multiplyScoreBonusConfig.MultiplierTime);

                    return multiplyScoreBonus;
                default:
                    throw new Exception($"There are no bonus to get in object pool for {bonusType}");
            }
        }

        public void RemoveBonus(Bonus bonus) {
            switch (bonus.BonusType) {
                case BonusType.KillAllBalls:
                    _objectPoolKillAllBallsBonus.ReturnObject(bonus as KillAllBallsBonus);
                    break;
                case BonusType.AddLive:
                    _objectPoolAddLiveBonus.ReturnObject(bonus as AddLiveBonus);
                    break;
                case BonusType.MultiplyScore:
                    _objectPoolMultiplyScoreBonus.ReturnObject(bonus as MultiplyScoreBonus);
                    break;
                default:
                    throw new Exception($"There are no bonus to delete in object pool for {bonus.BonusType}");
            }
        }

        KillAllBallsBonus KillAllBallsBonusFactoryMethod() => Instantiate(_bonusConfig.KillAllBallsBonusDescription.Prefab);
        AddLiveBonus AddLiveBonusFactoryMethod() => Instantiate(_bonusConfig.AddLiveBonusDescription.Prefab);
        MultiplyScoreBonus MultiplyScoreBonusFactoryMethod() => Instantiate(_bonusConfig.MultiplyScoreBonusDescription.Prefab);
        
        void TurnOnBonus(Bonus bonus) => bonus.gameObject.SetActive(true);
        
        void TurnOffBonus(Bonus bonus) => bonus.gameObject.SetActive(false);
    }
}