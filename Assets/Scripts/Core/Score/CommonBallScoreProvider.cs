using Configs;

namespace Core.Score {
    public sealed class CommonBallScoreProvider : BallScoreProvider {
        readonly GameConfig _gameConfig;
        
        public CommonBallScoreProvider(GameConfig gameConfig) {
            _gameConfig = gameConfig;
        }

        public override int GetBallScore() {
            return _gameConfig.Score;
        }
    }
}