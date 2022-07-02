using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class GameStarter : MonoBehaviour {
        void Awake() {
            GameState.TryCreate();
            
            GoToMainMenuScene();
        }

        void GoToMainMenuScene() {
            SceneManager.LoadScene("Level");
        }
    }
}