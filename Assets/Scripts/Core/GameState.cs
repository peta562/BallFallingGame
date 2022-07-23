using System.Collections.Generic;
using Configs;
using Core.Level;
using Core.Progress;
using Core.Lives;
using Core.PlayableObjects;
using Core.Score;
using UnityEngine;

namespace Core {
    public sealed class GameState {
        public static GameState Instance { get; private set; }

        const string ConfigsPath = "Configs";
        
        readonly List<BaseController> _controllers = new List<BaseController>();

        public BallsController BallsController { get; private set; }
        public BonusController BonusController { get; private set; }
        public LivesController LivesController { get; private set; }
        public ScoreController ScoreController { get; private set; }
        public LevelController LevelController { get; private set; }
        public ProgressController ProgressController { get; private set; }

        public GameConfig GameConfig { get; private set; }
        public PlayableObjectsConfig PlayableObjectsConfig { get; private set; }
        public ProgressConfig ProgressConfig { get; private set; }

        GameState() {

            LoadConfigs();
            AddControllers();
        }

        void LoadConfigs() {
            GameConfig = Resources.Load<GameConfig>($"{ConfigsPath}/GameConfig");
            PlayableObjectsConfig = Resources.Load<PlayableObjectsConfig>($"{ConfigsPath}/PlayableObjectsConfig");
            ProgressConfig = Resources.Load<ProgressConfig>($"{ConfigsPath}/ProgressConfig");
        }
        
        void AddControllers() {
            LivesController = Add(new LivesController(GameConfig));
            ScoreController = Add(new ScoreController(GameConfig));
            LevelController = Add(new LevelController(GameContext.Instance.WindowManager));
            BallsController = Add(new BallsController(GameConfig, PlayableObjectsConfig));
            BonusController = Add(new BonusController(GameConfig, PlayableObjectsConfig));
            ProgressController = Add(new ProgressController(ProgressConfig, ScoreController, LevelController, GameContext.Instance.WindowManager));
        }
        
        T Add<T>(T controller) where T : BaseController {
            _controllers.Add(controller);
            return controller;
        }
        
        public static GameState TryCreate() {
            if ( Instance == null ) {
                Instance = new GameState();
            }
            return Instance;
        }
    }
}