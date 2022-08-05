using Configs;

namespace Core.Score {
    public sealed class ConfigBallScoreProvider : IScoreProvider {
        readonly GameConfig _gameConfig;

        int _configScore;
        
        public ConfigBallScoreProvider(GameConfig gameConfig) {
            _gameConfig = gameConfig;
        }

        public void Init() {
            _configScore = _gameConfig.Score;
        }

        public void DeInit() {
            _configScore = 0;
        }

        public int GetScore() {
            return _configScore;
        }
    }
}