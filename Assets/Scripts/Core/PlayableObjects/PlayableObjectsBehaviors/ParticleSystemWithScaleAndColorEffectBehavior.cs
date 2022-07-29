using Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces;
using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors {
    public class ParticleSystemWithScaleAndColorEffectBehavior : IParticleEffectBehavior {
        readonly Color _color;
        readonly float _scale;
        
        ParticleSystem _particleSystem;

        public ParticleSystemWithScaleAndColorEffectBehavior(Color color, float scale) {
            _color = color;
            _scale = scale;
        }

        public void PlayEffect(ParticleSystem particleSystem) {
            _particleSystem = particleSystem;
            var main = particleSystem.main;
            main.startSize = _scale / 4;
            main.startColor = _color;
            _particleSystem.Play();
        }
        
        public void OnPauseChanged(bool isPaused) {
            if ( _particleSystem == null ) {
                return;
            }
            
            var main = _particleSystem.main;
            main.simulationSpeed = isPaused ? 0f : 1f;
        }
    }
}