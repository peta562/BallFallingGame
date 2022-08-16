using Core.UI.MainMenuUI;
using Core.UI.WindowsUI;
using UnityEngine;

namespace Core.Starters {
    public class MainMenuStarter : MonoBehaviour {
        [SerializeField] MainMenuUI MainMenuUi;

        WindowManager _windowManager;

        void Awake() {
            var gameState = GameState.Instance;
            
            _windowManager = GameContext.Instance.WindowManager;
            _windowManager.Init("MainMenuWindows");

            InitUI(gameState);
        }
        
        void OnDestroy() {
            _windowManager.DeInit();
            _windowManager = null;

            DeInitUI();
        }

        void InitUI(GameState gameState) {
            MainMenuUi.Init(gameState.ProgressController, gameState.SettingsController, _windowManager);
        }

        void DeInitUI() {
            MainMenuUi.DeInit();
        }
    }
}