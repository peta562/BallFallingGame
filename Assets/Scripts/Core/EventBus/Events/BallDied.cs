using Core.Balls;

namespace Core.EventBus.Events {
    public readonly struct BallDied {
        public readonly Ball Ball;

        public BallDied(Ball ball) {
            Ball = ball;
        }
    }
}