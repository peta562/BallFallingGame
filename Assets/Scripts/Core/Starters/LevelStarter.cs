﻿using Core.GameManagers;
using Core.PlayableObjects;
using Core.UI.LevelUI;
using Core.UI.WindowsUI;
using UnityEngine;

namespace Core.Starters {
    public class LevelStarter : MonoBehaviour {
        const string BundleName = "LevelWindows";
        
        [SerializeField] PlayableObjectFactory PlayableObjectFactory;
        [SerializeField] LevelUI LevelUi;

        WindowManager _windowManager;
        LevelManager _levelManager;

        void Awake() {
            var gameState = GameState.Instance;

            _windowManager = GameContext.Instance.WindowManager;
            _windowManager.Init(BundleName);

            gameState.BallsController.SetFactory(PlayableObjectFactory);
            gameState.BonusController.SetFactory(PlayableObjectFactory);
            
            _levelManager = new LevelManager(gameState.ScoreController, gameState.LivesController,
                gameState.ProgressController, gameState.BallsController, gameState.BonusController, gameState.LevelController);
            _levelManager.Init();
            
            InitUI(gameState);
        }

        void OnDestroy() {
            _windowManager.DeInit();
            _windowManager = null;
            
            _levelManager.DeInit();
            
            DeInitUI();
        }

        void Update() {
            _levelManager.Update();
        }

        void InitUI(GameState gameState) {
            LevelUi.Init(gameState.LivesController, gameState.ScoreController, _windowManager);
        }

        void DeInitUI() {
            LevelUi.DeInit();
        }
    }
}