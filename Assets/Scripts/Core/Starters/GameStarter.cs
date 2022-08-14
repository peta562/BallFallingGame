using Core.Scenes;
using UnityEngine;

namespace Core.Starters {
    public class GameStarter : MonoBehaviour {
        void Awake() {
            GameContext.TryCreate();
            GameState.TryCreate();
            
            GoToMainMenuScene();
        }

        async void GoToMainMenuScene() {
            await SceneLoader.Instance.LoadLoadingScreen();
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
    }
}