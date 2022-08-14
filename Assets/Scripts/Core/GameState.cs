﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Configs;
using Core.Level;
using Core.Progress;
using Core.Lives;
using Core.Loaders;
using Core.PlayableObjects;
using Core.SaveLoad;
using Core.Score;
using Core.Settings;

namespace Core {
    public sealed class GameState {
        public static GameState Instance { get; private set; }

        readonly List<BaseController> _controllers = new List<BaseController>();

        public BallsController BallsController { get; private set; }
        public BonusController BonusController { get; private set; }
        public LivesController LivesController { get; private set; }
        public ScoreController ScoreController { get; private set; }
        public LevelController LevelController { get; private set; }
        public ProgressController ProgressController { get; private set; }
        public SettingsController SettingsController { get; private set; }

        GameConfig GameConfig { get; set; }
        PlayableObjectsConfig PlayableObjectsConfig { get; set; }
        ProgressConfig ProgressConfig { get; set; }

        SaveData _saveData;
        
        IConfigLoader ConfigLoader => GameContext.Instance.ConfigLoader;

        GameState() {
            CreateGameState();
        }

        public void DestroyGameState() {
            SaveData();
            RemoveControllers();
            UnloadConfigs();
        }

        async void CreateGameState() {
            await LoadConfigs();
            AddControllers();
            LoadData();
        }

        async Task LoadConfigs() {
            GameConfig = await ConfigLoader.Load<GameConfig>("GameConfig");
            PlayableObjectsConfig = await ConfigLoader.Load<PlayableObjectsConfig>("PlayableObjectsConfig");
            ProgressConfig = await ConfigLoader.Load<ProgressConfig>("ProgressConfig");
        }

        void UnloadConfigs() {
            ConfigLoader.Unload(GameConfig);
            ConfigLoader.Unload(PlayableObjectsConfig);
            ConfigLoader.Unload(ProgressConfig);
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

        void RemoveControllers() {
            for (var i = 0; i < _controllers.Count; i++) {
                _controllers[i] = null;
                _controllers.Remove(_controllers[i]);
            }
        }
        
        void SaveData() {
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