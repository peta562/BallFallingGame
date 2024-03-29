namespace Core {
    public class FocusChangeDetector : Singleton<FocusChangeDetector> {
        void OnApplicationFocus(bool focus) {
            if ( !focus ) {
                GameState.Instance.Save();
            }
        }

        private void OnApplicationQuit() {
            GameState.Instance.Save();
        }
    }
}