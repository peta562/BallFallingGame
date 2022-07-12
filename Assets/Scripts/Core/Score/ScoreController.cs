﻿using Configs;
using Core.EventBus;
using Core.EventBus.Events;

namespace Core.Score {
    public sealed class ScoreController : BaseController {
        readonly GameConfig _gameConfig;
        readonly IBallScoreProvider _ballScoreProvider;
        
        int _score;

        public int Score => _score;

        public ScoreController(GameConfig gameConfig) {
            _gameConfig = gameConfig;

            _ballScoreProvider = new ConfigBallScoreProvider(gameConfig);
        }
        
        public override void Init() {
            _score = 0;
            EventManager.Instance.Subscribe<BallKilled>(this, OnBallKilled);
        }

        public override void DeInit() {
            EventManager.Instance.Unsubscribe<BallKilled>(OnBallKilled);
        }

        void OnBallKilled(BallKilled ev) {
            _score += _ballScoreProvider.GetBallScore();
            EventManager.Instance.Fire(new ScoreChanged(_score));
        }
    }
}