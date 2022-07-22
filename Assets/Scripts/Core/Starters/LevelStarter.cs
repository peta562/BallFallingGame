using Core.Balls;
using Core.Bonuses;
using Core.GameManagers;
using Core.UI.LevelUI;
using Core.UI.Windows;
using UnityEngine;

namespace Core.Starters {
    public class LevelStarter : MonoBehaviour {
        [SerializeField] BallFactory BallFactory;
        [SerializeField] BonusFactory BonusFactory;
        [SerializeField] LevelUI LevelUi;
        [SerializeField] WindowHolder WindowHolder;

        WindowManager _windowManager;
        LevelManager _levelManager;

        void Awake() {
            var gameState = GameState.Instance;

            _windowManager = GameContext.Instance.WindowManager;
            _windowManager.Init(WindowHolder.Windows, WindowHolder.WindowBackground);

            gameState.BallsController.ChangeFactory(BallFactory);
            gameState.BonusController.ChangeFactory(BonusFactory);
            
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