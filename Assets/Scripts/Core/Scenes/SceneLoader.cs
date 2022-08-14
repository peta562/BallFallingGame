using System;
using System.Threading.Tasks;
using Core.Loaders;
using UnityEngine.SceneManagement;

namespace Core.Scenes {
    public sealed class SceneLoader : Singleton<SceneLoader> {
        LoadingScreen _loadingScreen;
        float _targetProgress;
        bool _isSceneLoading;

        AssetLoader AssetLoader => GameContext.Instance.AssetLoader;

        public async Task LoadLoadingScreen() {
            _loadingScreen = await AssetLoader.LoadAsset<LoadingScreen>("LoadingScreen", transform);
        }
        
        public async void LoadScene(SceneNames sceneName) {
            var strSceneName = sceneName.ToString();
            
            _loadingScreen.Init();
            
            var scene = SceneManager.LoadSceneAsync(strSceneName);
            scene.allowSceneActivation = false;

            _isSceneLoading = true;

            do {
                await Task.Delay(100);

                _targetProgress = scene.progress;
            } while ( scene.progress < 0.9f );

            await Task.Delay(1000);

            scene.allowSceneActivation = true;
            _isSceneLoading = false;
            _loadingScreen.DeInit();
        }

        void OnDestroy() {
            AssetLoader.UnloadAsset(_loadingScreen.gameObject);
        }

        void Update() {
            if ( !_isSceneLoading ) {
                return;
            }
            _loadingScreen.SetProgress(_targetProgress);
        }
    }
}