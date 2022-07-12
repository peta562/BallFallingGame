﻿using Configs;
using Core.EventBus;
using Core.EventBus.Events;

namespace Core.Lives {
    public sealed class LivesController : BaseController {
        readonly GameConfig _gameConfig;
        
        int _lives;

        public int Lives => _lives;

        public LivesController(GameConfig gameConfig) {
            _gameConfig = gameConfig;
        }
        
        public override void Init() {
            _lives = _gameConfig.Lives;
            
            EventManager.Instance.Subscribe<BallFell>(this, OnBallFell);
        }

        public override void DeInit() {
            EventManager.Instance.Unsubscribe<BallFell>(OnBallFell);
        }

        void OnBallFell(BallFell ev) {
            _lives -= 1;
            
            EventManager.Instance.Fire(new LivesChanged(_lives));
        }
    }
}