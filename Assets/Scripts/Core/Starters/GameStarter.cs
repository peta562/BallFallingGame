using Core.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Starters {
    public class GameStarter : MonoBehaviour {
        void Awake() {
            GameState.TryCreate();
            
            GoToMainMenuScene();
        }

        void GoToMainMenuScene() {
            SceneLoader.Instance.LoadScene(SceneNames.MainMenu);
        }
    }
}