using Configs;

namespace Core.Score {
    public sealed class ConfigBallScoreProvider : IBallScoreProvider {
        readonly GameConfig _gameConfig;
        
        public ConfigBallScoreProvider(GameConfig gameConfig) {
            _gameConfig = gameConfig;
        }

        public int GetBallScore() {
            return _gameConfig.Score;
        }
    }
}