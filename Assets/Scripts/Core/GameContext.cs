using Core.Loaders;
using Core.Pause;
using Core.SaveLoad;
using Core.Scenes;
using Core.UI.WindowsUI;

namespace Core {
    public sealed class GameContext {
        public static GameContext Instance { get; private set; }
        
        public ISaveLoadManager SaveLoadManager { get; }
        public PrefabLoader PrefabLoader { get; }
        public SceneLoader SceneLoader { get; }
        public IConfigLoader ConfigLoader { get; }
        public BundleLoader BundleLoader { get; }
        public PauseManager PauseManager { get; }
        public WindowManager WindowManager { get; }

        GameContext() {
            SaveLoadManager = new JsonSaveLoadManager();
            PrefabLoader = new PrefabLoader();
            SceneLoader = new SceneLoader();
            ConfigLoader = new ConfigLoader();
            BundleLoader = new BundleLoader();
            PauseManager = new PauseManager();
            WindowManager = new WindowManager();
        }
        
        public static GameContext TryCreate() {
            if ( Instance == null ) {
                Instance = new GameContext();
            }
            return Instance;
        }
    }
}