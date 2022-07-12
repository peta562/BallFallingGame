﻿using Core.UI.MainMenuUI;
using Core.UI.Windows;
using UnityEngine;

namespace Core.Starters {
    public class MainMenuStarter : MonoBehaviour {
        [SerializeField] MainMenuUI MainMenuUi;
        [SerializeField] WindowHolder WindowHolder;

        WindowManager _windowManager;

        void Awake() {
            var gameState = GameState.Instance;
            
            _windowManager = gameState.WindowManager;
            _windowManager.Init(WindowHolder.Windows, WindowHolder.WindowBackground);

            InitUI(gameState);
        }
        
        void OnDestroy() {
            _windowManager.DeInit();
            _windowManager = null;

            DeInitUI();
        }

        void Update() {
            
        }

        void InitUI(GameState gameState) {
            MainMenuUi.Init(gameState.ProgressController);
        }

        void DeInitUI() {
            MainMenuUi.DeInit();
        }
    }
}