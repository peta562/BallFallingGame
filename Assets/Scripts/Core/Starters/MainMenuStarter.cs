using System.Collections.Generic;
using Core.UI.MainMenuUI;
using Core.UI.Windows;
using UnityEngine;

namespace Core.Starters {
    public class MainMenuStarter : MonoBehaviour {
        [SerializeField] MainMenuUI MainMenuUi;
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
            _controllers.Add(gameState.ProgressController);
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
            MainMenuUi.Init(gameState.ProgressController);
        }

        void DeInitUI() {
            MainMenuUi.DeInit();
        }
    }
}