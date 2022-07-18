using Core.Pause;
using Core.UI.Windows;

namespace Core {
    public sealed class GameContext {
        public static GameContext Instance { get; private set; }
        
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