using System.Collections.Generic;
using Configs;
using Core.Balls;
using Core.Score;
using UnityEngine;

namespace Core {
    public class GameState {
        public static GameState Instance { get; private set; }
        readonly List<BaseController> _controllers = new List<BaseController>();
        
        public BallsController BallsController { get; private set; }
        public LivesController LivesController { get; private set; }
        public ScoreController ScoreController { get; private set; }
        
        public GameConfig GameConfig { get; private set; }
        public BallConfig BallConfig { get; private set; }
        
        GameState() {
            //add pause

            LoadConfigs();
            AddControllers();
        }

        void LoadConfigs() {
            GameConfig = Resources.Load<GameConfig>("Configs/GameConfig");
            BallConfig = Resources.Load<BallConfig>("Configs/BallConfig");
        }
        
        void AddControllers() {
            BallsController = Add(new BallsController(GameConfig));
            LivesController = Add(new LivesController(GameConfig));
            ScoreController = Add(new ScoreController(GameConfig));
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