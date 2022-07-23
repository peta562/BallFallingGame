using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces {
    public interface IParticleEffectBehavior {
        public void PlayEffect(ParticleSystem particleSystem);

        public void OnPauseChanged(bool isPaused);
    }
}