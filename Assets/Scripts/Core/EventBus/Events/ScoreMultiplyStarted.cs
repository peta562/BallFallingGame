namespace Core.EventBus.Events {
    public readonly struct ScoreMultiplyStarted {
        public readonly int Multiplier;
        public readonly float MultiplierTime;

        public ScoreMultiplyStarted(int multiplier, float multiplierTime) {
            Multiplier = multiplier;
            MultiplierTime = multiplierTime;
        }
    }
}