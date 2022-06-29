using Core.Balls.BallEntity;

namespace Core.EventBus.Events {
    public readonly struct BallKilled {
        public readonly Ball Ball;

        public BallKilled(Ball ball) {
            Ball = ball;
        }
    }
}