using Core.Pause;
using Core.UI.WindowsUI;

namespace Core {
    public sealed class GameContext {
        public static GameContext Instance { get; private set; }
        
        public SaveLoadManager SaveLoadManager { get; }
        public PauseManager PauseManager { get; }
        public WindowManager WindowManager { get; }

        GameContext() {
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