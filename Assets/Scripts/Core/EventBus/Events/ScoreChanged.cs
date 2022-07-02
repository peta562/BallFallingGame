namespace Core.EventBus.Events {
    public readonly struct ScoreChanged {
        public readonly int Score;

        public ScoreChanged(int score) => Score = score;
    }
}