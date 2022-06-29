using Configs;
using Core.EventBus;
using Core.EventBus.Events;

namespace Core.Score {
    public sealed class ScoreController : BaseController {
        readonly GameConfig _gameConfig;
        readonly BallScoreProvider _ballScoreProvider;
        
        int _score;

        public ScoreController(GameConfig gameConfig) {
            _gameConfig = gameConfig;

            _ballScoreProvider = new CommonBallScoreProvider(gameConfig);
        }
        
        public override void Init() {
            EventManager.Instance.Subscribe<BallKilled>(this, OnBallKilled);
        }

        public override void DeInit() {
            EventManager.Instance.Unsubscribe<BallKilled>(OnBallKilled);
        }

        void OnBallKilled(BallKilled ev) {
            _score += _ballScoreProvider.GetBallScore();
        }
    }
}