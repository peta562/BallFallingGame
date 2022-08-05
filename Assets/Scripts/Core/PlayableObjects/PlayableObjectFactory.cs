using System;
using Configs;
using Core.ObjectPool;
using Core.PlayableObjects.Balls;
using Core.PlayableObjects.Bonuses;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.PlayableObjects {
    public sealed class PlayableObjectFactory : MonoBehaviour {
        const int InitialStockCount = 5;

        PlayableObjectsConfig _playableObjectsConfig;
        
        ObjectPool<Ball> _objectPoolBall; 
        ObjectPool<KillAllBallsBonus> _objectPoolKillAllBallsBonus;
        ObjectPool<AddLiveBonus> _objectPoolAddLiveBonus;
        ObjectPool<MultiplyScoreBonus> _objectPoolMultiplyScoreBonus;

        public void Init(PlayableObjectsConfig playableObjectsConfig) {
            _playableObjectsConfig = playableObjectsConfig;

            CreateObjectPools();
        }

        public PlayableObject GetPlayableObject(PlayableObjectType playableObjectType, Vector2 stageDimensions) {
            float scale;
            Vector2 position;

            switch (playableObjectType) {
                case PlayableObjectType.Ball:
                    var ball = _objectPoolBall.GetObject();

                    var ballConfig = _playableObjectsConfig.BallDescription;
                    scale = ballConfig.Scale.GetRandomFloat;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);
                    
                    var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                    var health = ballConfig.Health.GetRandomInt;

                    ball.Init(playableObjectType, health, position, scale, ballConfig.Sprite, color);

                    return ball;
                case PlayableObjectType.KillAllBallsBonus:
                    var killAllBallsBonus = _objectPoolKillAllBallsBonus.GetObject();

                    var killAllBallsBonusConfig = _playableObjectsConfig.KillAllBallsBonusDescription;
                    scale = killAllBallsBonusConfig.Scale;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);

                    killAllBallsBonus.Init(playableObjectType, killAllBallsBonusConfig.Health, position, scale,
                        killAllBallsBonusConfig.Sprite);

                    return killAllBallsBonus;
                case PlayableObjectType.AddLiveBonus:
                    var addLiveBonus = _objectPoolAddLiveBonus.GetObject();

                    var addLiveBonusConfig = _playableObjectsConfig.AddLiveBonusDescription;
                    scale = addLiveBonusConfig.Scale;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);

                    addLiveBonus.Init(playableObjectType, addLiveBonusConfig.Health, position, scale, addLiveBonusConfig.Sprite,
                        addLiveBonusConfig.LivesToAdd);

                    return addLiveBonus;
                case PlayableObjectType.MultiplyScoreBonus:
                    var multiplyScoreBonus = _objectPoolMultiplyScoreBonus.GetObject();

                    var multiplyScoreBonusConfig = _playableObjectsConfig.MultiplyScoreBonusDescription;
                    scale = multiplyScoreBonusConfig.Scale;
                    position = new Vector2(Random.Range(-stageDimensions.x + scale, stageDimensions.x - scale),
                        stageDimensions.y + scale);

                    multiplyScoreBonus.Init(playableObjectType, multiplyScoreBonusConfig.Health, position, scale,
                        multiplyScoreBonusConfig.Sprite, multiplyScoreBonusConfig.Multiplier,
                        multiplyScoreBonusConfig.MultiplierTime);

                    return multiplyScoreBonus;
                default:
                    throw new Exception($"There are no playableObject to get in object pool for {playableObjectType}");
            }
        }

        public void RemovePlayableObject(PlayableObject playableObject) {
            switch (playableObject.PlayableObjectType) {
                case PlayableObjectType.Ball:
                    _objectPoolBall.ReturnObject(playableObject as Ball);
                    break;
                case PlayableObjectType.KillAllBallsBonus:
                    _objectPoolKillAllBallsBonus.ReturnObject(playableObject as KillAllBallsBonus);
                    break;
                case PlayableObjectType.AddLiveBonus:
                    _objectPoolAddLiveBonus.ReturnObject(playableObject as AddLiveBonus);
                    break;
                case PlayableObjectType.MultiplyScoreBonus:
                    _objectPoolMultiplyScoreBonus.ReturnObject(playableObject as MultiplyScoreBonus);
                    break;
                default:
                    throw new Exception($"There are no playableObject to delete in object pool for {playableObject.PlayableObjectType}");
            }
        }

        void CreateObjectPools() {
            _objectPoolBall = new ObjectPool<Ball>(BallFactoryMethod, TurnOnPlayableObject, TurnOffPlayableObject,
                InitialStockCount);
            _objectPoolKillAllBallsBonus = new ObjectPool<KillAllBallsBonus>(KillAllBallsBonusFactoryMethod,
                TurnOnPlayableObject, TurnOffPlayableObject, InitialStockCount);
            _objectPoolAddLiveBonus = new ObjectPool<AddLiveBonus>(AddLiveBonusFactoryMethod, TurnOnPlayableObject,
                TurnOffPlayableObject, InitialStockCount);
            _objectPoolMultiplyScoreBonus = new ObjectPool<MultiplyScoreBonus>(MultiplyScoreBonusFactoryMethod,
                TurnOnPlayableObject, TurnOffPlayableObject, InitialStockCount);
        }

        Ball BallFactoryMethod() => Instantiate(_playableObjectsConfig.BallDescription.Prefab);
        KillAllBallsBonus KillAllBallsBonusFactoryMethod() => Instantiate(_playableObjectsConfig.KillAllBallsBonusDescription.Prefab);
        AddLiveBonus AddLiveBonusFactoryMethod() => Instantiate(_playableObjectsConfig.AddLiveBonusDescription.Prefab);
        MultiplyScoreBonus MultiplyScoreBonusFactoryMethod() => Instantiate(_playableObjectsConfig.MultiplyScoreBonusDescription.Prefab);

        void TurnOnPlayableObject(PlayableObject playableObject) => playableObject.gameObject.SetActive(true);
        
        void TurnOffPlayableObject(PlayableObject playableObject) => playableObject.gameObject.SetActive(false);
    }
}