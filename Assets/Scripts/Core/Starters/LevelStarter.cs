﻿using System.Collections.Generic;
using Core.Balls;
using Core.UI.LevelUI;
using Core.UI.Windows;
using UnityEngine;

namespace Core.Starters {
    public class LevelStarter : MonoBehaviour {
        [SerializeField] BallFactory BallFactory;
        [SerializeField] LevelUI LevelUi;
        [SerializeField] WindowHolder WindowHolder;

        readonly List<BaseController> _controllers = new List<BaseController>();

        WindowManager _windowManager;
        
        bool IsPaused => GameState.Instance.PauseManager.IsPaused;

        void Awake() {
            var gameState = GameState.Instance;

            _windowManager = gameState.WindowManager;
            _windowManager.Init(WindowHolder.Windows, WindowHolder.WindowBackground);
            
            AddControllers(gameState);
            InitControllers();
            
            InitUI(gameState);
        }

        void OnDestroy() {
            _windowManager.DeInit();
            _windowManager = null;

            DeInitControllers();
            
            DeInitUI();
        }

        void AddControllers(GameState gameState) {
            _controllers.Add(gameState.LivesController);
            _controllers.Add(gameState.ScoreController);
            _controllers.Add(gameState.LevelController);
            
            _controllers.Add(gameState.BallsController);
            gameState.BallsController.ChangeFactory(BallFactory);
        }
        
        void InitControllers() {
            foreach (var controller in _controllers) {
                controller.Init();
            }
        }

        void Update() {
            if ( IsPaused ) {
                return;
            }
            
            foreach (var controller in _controllers) {
                controller.Update();
            }
        }

        void DeInitControllers() {
            foreach (var controller in _controllers) {
                controller.DeInit();
            }
        }
        
        void InitUI(GameState gameState) {
            LevelUi.Init(gameState.LivesController, gameState.ScoreController, _windowManager);
        }

        void DeInitUI() {
            LevelUi.DeInit();
        }
    }
}