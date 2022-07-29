using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scenes {
    public sealed class SceneLoader : Singleton<SceneLoader> {
        [SerializeField] LoadingScreen LoadingScreen;

        float _targetProgress;
        bool _isSceneLoading;
        
        public async void LoadScene(SceneNames sceneName) {
            var strSceneName = sceneName.ToString();
            var scene = SceneManager.LoadSceneAsync(strSceneName);
            scene.allowSceneActivation = false;

            LoadingScreen.Init();
            _isSceneLoading = true;

            do {
                await Task.Delay(100);

                _targetProgress = scene.progress;
            } while ( scene.progress < 0.9f );

            await Task.Delay(1000);

            scene.allowSceneActivation = true;
            _isSceneLoading = false;
            LoadingScreen.DeInit();
        }

        void Update() {
            if ( !_isSceneLoading ) {
                return;
            }
            LoadingScreen.SetProgress(_targetProgress);
        }
    }
}