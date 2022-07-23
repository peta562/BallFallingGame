using Core.PlayableObjects;

namespace Core.EventBus.Events {
    public readonly struct PlayableObjectKilled {
        public readonly PlayableObject PlayableObject;

        public PlayableObjectKilled(PlayableObject playableObject) {
            PlayableObject = playableObject;
        }
    }
}