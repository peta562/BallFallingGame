using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scenes {
    public sealed class SceneLoader : Singleton<SceneLoader> {
        [SerializeField] LoadingScreen LoadingScreen;

        float _targetProgress;
        
        public async void LoadScene(string sceneName) {
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            LoadingScreen.Init();

            do {
                await Task.Delay(100);

                _targetProgress = scene.progress;
            } while ( scene.progress < 0.9f );

            await Task.Delay(1000);

            scene.allowSceneActivation = true;
            LoadingScreen.DeInit();
        }

        void Update() {
            LoadingScreen.SetProgress(_targetProgress);
        }
    }
}