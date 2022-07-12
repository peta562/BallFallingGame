using System.Collections.Generic;
using Configs;
using Core.Balls;
using Core.Level;
using Core.Progress;
using Core.Lives;
using Core.Pause;
using Core.Score;
using Core.UI.Windows;
using UnityEngine;

namespace Core {
    public class GameState {
        public static GameState Instance { get; private set; }

        const string ConfigsPath = "Configs";
        
        readonly List<BaseController> _controllers = new List<BaseController>();
        
        public PauseManager PauseManager { get; }
        public WindowManager WindowManager { get; }
        
        public BallsController BallsController { get; private set; }
        public LivesController LivesController { get; private set; }
        public ScoreController ScoreController { get; private set; }
        public LevelController LevelController { get; private set; }
        public ProgressController ProgressController { get; private set; }

        public GameConfig GameConfig { get; private set; }
        public BallConfig BallConfig { get; private set; }
        public ProgressConfig ProgressConfig { get; private set; }

        GameState() {
            PauseManager = new PauseManager();
            WindowManager = new WindowManager();

            LoadConfigs();
            AddControllers();
        }

        void LoadConfigs() {
            GameConfig = Resources.Load<GameConfig>($"{ConfigsPath}/GameConfig");
            BallConfig = Resources.Load<BallConfig>($"{ConfigsPath}/BallConfig");
            ProgressConfig = Resources.Load<ProgressConfig>($"{ConfigsPath}/ProgressConfig");
        }
        
        void AddControllers() {
            LivesController = Add(new LivesController(GameConfig));
            ScoreController = Add(new ScoreController(GameConfig));
            LevelController = Add(new LevelController(WindowManager));
            BallsController = Add(new BallsController(GameConfig, BallConfig));
            ProgressController = Add(new ProgressController(ProgressConfig, ScoreController, LevelController, WindowManager));
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