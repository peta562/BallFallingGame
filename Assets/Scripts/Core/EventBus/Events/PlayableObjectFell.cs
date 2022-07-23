using Core.PlayableObjects;

namespace Core.EventBus.Events {
    public readonly struct PlayableObjectFell {
        public readonly PlayableObject PlayableObject;

        public PlayableObjectFell(PlayableObject playableObject) {
            PlayableObject = playableObject;
        }
    }
}