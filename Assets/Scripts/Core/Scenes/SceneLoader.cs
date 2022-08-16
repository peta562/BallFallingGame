using System.Threading.Tasks;
using Core.Loaders;
using UnityEngine.SceneManagement;

namespace Core.Scenes {
    public sealed class SceneLoader {
        LoadingScreen _loadingScreen;

        PrefabLoader PrefabLoader => GameContext.Instance.PrefabLoader;

        public async void LoadScene(SceneNames sceneName) {
            var strSceneName = sceneName.ToString();
            
            _loadingScreen = await PrefabLoader.LoadAsset<LoadingScreen>("LoadingScreen");
            _loadingScreen.Init();
            
            var scene = SceneManager.LoadSceneAsync(strSceneName);
            scene.allowSceneActivation = false;

            do {
                _loadingScreen.SetProgress(scene.progress);
            } while ( scene.progress < 0.9f );

            await Task.Delay(1000);
            
            scene.allowSceneActivation = true;
            
            _loadingScreen.DeInit();
            PrefabLoader.UnloadAsset(_loadingScreen);
        }
    }
}