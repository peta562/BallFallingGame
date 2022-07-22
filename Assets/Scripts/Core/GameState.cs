using System.Collections.Generic;
using Configs;
using Core.Balls;
using Core.Bonuses;
using Core.Level;
using Core.Progress;
using Core.Lives;
using Core.Pause;
using Core.Score;
using Core.UI.Windows;
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
        public BallConfig BallConfig { get; private set; }
        public BonusConfig BonusConfig { get; private set; }
        public ProgressConfig ProgressConfig { get; private set; }

        GameState() {

            LoadConfigs();
            AddControllers();
        }

        void LoadConfigs() {
            GameConfig = Resources.Load<GameConfig>($"{ConfigsPath}/GameConfig");
            BallConfig = Resources.Load<BallConfig>($"{ConfigsPath}/BallConfig");
            BonusConfig = Resources.Load<BonusConfig>($"{ConfigsPath}/BonusConfig");
            ProgressConfig = Resources.Load<ProgressConfig>($"{ConfigsPath}/ProgressConfig");
        }
        
        void AddControllers() {
            LivesController = Add(new LivesController(GameConfig));
            ScoreController = Add(new ScoreController(GameConfig));
            LevelController = Add(new LevelController(GameContext.Instance.WindowManager));
            BallsController = Add(new BallsController(GameConfig, BallConfig));
            BonusController = Add(new BonusController(GameConfig, BonusConfig));
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