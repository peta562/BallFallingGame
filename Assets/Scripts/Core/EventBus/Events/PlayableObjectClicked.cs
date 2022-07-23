using Core.PlayableObjects;

namespace Core.EventBus.Events {
    public readonly struct PlayableObjectClicked {
        public readonly PlayableObject PlayableObject;

        public PlayableObjectClicked(PlayableObject playableObject) {
            PlayableObject = playableObject;
        }
    }
}