namespace Core {
    public class QuitManager : Singleton<QuitManager> {
        void OnApplicationQuit() {
            GameState.Instance.DestroyGameState();
        }
    }
}