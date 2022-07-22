using UnityEngine;

namespace Core.GameBehaviors.Interfaces {
    public interface IParticleEffectBehavior {
        public void PlayEffect(ParticleSystem particleSystem);

        public void OnPauseChanged(bool isPaused);
    }
}