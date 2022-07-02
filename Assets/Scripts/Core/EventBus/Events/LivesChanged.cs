namespace Core.EventBus.Events {
    public readonly struct LivesChanged {
        public readonly int Lives;

        public LivesChanged(int lives) => Lives = lives;
    }
}