using System.Collections.Generic;
using Configs;
using Core.Level;
using Core.Progress;
using Core.Lives;
using Core.PlayableObjects;
using Core.SaveLoad;
using Core.Score;
using Core.Settings;
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
        public SettingsController SettingsController { get; private set; }

        public GameConfig GameConfig { get; private set; }
        public PlayableObjectsConfig PlayableObjectsConfig { get; private set; }
        public ProgressConfig ProgressConfig { get; private set; }

        SaveData _saveData;

        GameState() {

            LoadConfigs();
            AddControllers();
            LoadData();
        }

        public void SaveData() {
            foreach (var controller in _controllers) {
                controller.Save(_saveData);
            }
            
            var saveLoadManager = GameContext.Instance.SaveLoadManager;
            saveLoadManager.Save(_saveData);
        }
        
        void LoadData() {
            var saveLoadManager = GameContext.Instance.SaveLoadManager;
            _saveData = saveLoadManager.Load();
            
            foreach (var controller in _controllers) {
                controller.Load(_saveData);
            }
        }
        
        void LoadConfigs() {
            GameConfig = Resources.Load<GameConfig>($"{ConfigsPath}/GameConfig");
            PlayableObjectsConfig = Resources.Load<PlayableObjectsConfig>($"{ConfigsPath}/PlayableObjectsConfig");
            ProgressConfig = Resources.Load<ProgressConfig>($"{ConfigsPath}/ProgressConfig");
        }
        
        void AddControllers() {
            var gameContext = GameContext.Instance;
            
            LivesController = Add(new LivesController(GameConfig));
            ScoreController = Add(new ScoreController(GameConfig));
            LevelController = Add(new LevelController(gameContext));
            BallsController = Add(new BallsController(GameConfig, PlayableObjectsConfig));
            BonusController = Add(new BonusController(GameConfig, PlayableObjectsConfig));
            ProgressController = Add(new ProgressController(ProgressConfig, ScoreController, LevelController, gameContext));
            SettingsController = Add(new SettingsController());
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