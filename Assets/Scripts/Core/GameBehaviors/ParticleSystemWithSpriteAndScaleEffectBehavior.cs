using Core.GameBehaviors.Interfaces;
using UnityEngine;

namespace Core.GameBehaviors {
    public sealed class ParticleSystemWithSpriteAndScaleEffectBehavior : IParticleEffectBehavior  {
        ParticleSystem _particleSystem;
        readonly Sprite _sprite;
        readonly float _scale;

        public ParticleSystemWithSpriteAndScaleEffectBehavior(Sprite sprite, float scale) {
            _sprite = sprite;
            _scale = scale;
        }

        public void PlayEffect(ParticleSystem particleSystem) {
            _particleSystem = particleSystem;
            _particleSystem.textureSheetAnimation.SetSprite(0, _sprite);
            var main = particleSystem.main;
            main.startSize = _scale / 3;
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