using Configs;
using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;

namespace Core.Score {
    public sealed class ScoreController : BaseController {
        readonly GameConfig _gameConfig;
        readonly IBallScoreProvider _ballScoreProvider;
        
        int _score;
        bool _isMultiplyScoreEnabled;
        int _multiplier;
        float _multiplierTime;
        float _deltaTime;
        
        public int Score => _score;

        public ScoreController(GameConfig gameConfig) {
            _gameConfig = gameConfig;

            _ballScoreProvider = new ConfigBallScoreProvider(gameConfig);
        }
        
        public override void Init() {
            _score = 0;
        }

        public override void Update() {
            if ( !_isMultiplyScoreEnabled ) {
                return;
            }
            
            _deltaTime += Time.deltaTime;

            if ( _deltaTime >= _multiplierTime ) {
                StopMultiplyScore();
            }
        }

        public override void DeInit() {
        }

        public void AddScore() {
            var addedScore = _ballScoreProvider.GetBallScore();

            if ( _isMultiplyScoreEnabled ) {
                addedScore *= _multiplier;
            }
            
            _score += addedScore;
            EventManager.Instance.Fire(new ScoreChanged(_score));
        }

        public void StartMultiplyScore(int multiplier, float multiplierTime) {
            _multiplier = multiplier;
            _multiplierTime = multiplierTime;
            _deltaTime = 0f;

            _isMultiplyScoreEnabled = true;
            EventManager.Instance.Fire(new ScoreMultiplyStarted(_multiplier, _multiplierTime));
        }

        void StopMultiplyScore() {
            _isMultiplyScoreEnabled = false;
            
            EventManager.Instance.Fire(new ScoreMultiplyStopped());
        }
    }
}