using Configs;

namespace Core.Score {
    public sealed class ConfigBallScoreProvider : BallScoreProvider {
        readonly GameConfig _gameConfig;
        
        public ConfigBallScoreProvider(GameConfig gameConfig) {
            _gameConfig = gameConfig;
        }

        public override int GetBallScore() {
            return _gameConfig.Score;
        }
    }
}