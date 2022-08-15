using Core.Scenes;
using UnityEngine;

namespace Core.Starters {
    public class GameStarter : MonoBehaviour {
        void Awake() {
            GameContext.TryCreate();
            GameState.TryCreate();
            
            GoToMainMenuScene();
        }

        void GoToMainMenuScene() {
            GameContext.Instance.SceneLoader.LoadScene(SceneNames.MainMenu);
        }
    }
}