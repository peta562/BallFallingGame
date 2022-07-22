using Configs;
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
            _lives = _gameConfig.MaxLives;
        }

        public override void DeInit() {
        }

        public void ReduceLive() {
            _lives -= 1;
            
            EventManager.Instance.Fire(new LivesChanged(_lives));
        }

        public void AddLives(int lives) {
            if ( _lives + lives >= _gameConfig.MaxLives ) {
                _lives = _gameConfig.MaxLives;
            } else {
                _lives += lives;
            }
            
            EventManager.Instance.Fire(new LivesChanged(_lives));
        }
    }
}