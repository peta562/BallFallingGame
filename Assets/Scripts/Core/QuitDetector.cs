namespace Core {
    public class QuitDetector : Singleton<QuitDetector> {
        void OnApplicationQuit() {
            GameState.Instance.DestroyGameState();
        }
    }
}