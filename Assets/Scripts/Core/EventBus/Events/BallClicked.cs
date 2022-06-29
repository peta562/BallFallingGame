using Core.Balls.BallEntity;

namespace Core.EventBus.Events {
    public readonly struct BallClicked {
        public readonly Ball Ball;

        public BallClicked(Ball ball) {
            Ball = ball;
        }
    }
}