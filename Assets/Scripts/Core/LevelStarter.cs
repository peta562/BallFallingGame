using System.Collections.Generic;
using Core.Balls;
using Core.UI.LevelUI;
using Core.UI.Windows;
using UnityEngine;

namespace Core {
    public class LevelStarter : MonoBehaviour {
        [SerializeField] BallFactory BallFactory;
        [SerializeField] LevelUI LevelUi;
        [SerializeField] WindowHolder WindowHolder;

        readonly List<BaseController> _controllers = new List<BaseController>();

        WindowManager _windowManager;
        
        bool IsPaused => GameState.Instance.PauseManager.IsPaused;

        void Awake() {
            var gameState = GameState.Instance;
            
            _windowManager = new WindowManager();
            _windowManager.Init(WindowHolder.Windows);
            
            AddControllers(gameState);
            InitControllers();
            
            InitUI(gameState);
        }

        void OnDestroy() {
            DeInitControllers();
            
            DeInitUI();
            
            _windowManager.DeInit();
        }

        void AddControllers(GameState gameState) {
            _controllers.Add(gameState.BallsController);
            gameState.BallsController.ChangeFactory(BallFactory);
            
            _controllers.Add(gameState.LivesController);
            _controllers.Add(gameState.ScoreController);
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