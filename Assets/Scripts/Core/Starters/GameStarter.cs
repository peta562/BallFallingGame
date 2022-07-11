using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Starters {
    public class GameStarter : MonoBehaviour {
        void Awake() {
            GameState.TryCreate();
            
            GoToMainMenuScene();
        }

        void GoToMainMenuScene() {
            SceneManager.LoadScene("MainMenu");
        }
    }
}