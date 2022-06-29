using Core.Balls;

namespace Core.EventBus.Events {
    public readonly struct BallFell {
        public readonly Ball Ball;

        public BallFell(Ball ball) {
            Ball = ball;
        }
    }
}