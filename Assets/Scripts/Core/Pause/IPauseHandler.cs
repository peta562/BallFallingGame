namespace Core.Pause {
    public interface IPauseHandler {
        void OnPauseChanged(bool isPaused);
    }
}