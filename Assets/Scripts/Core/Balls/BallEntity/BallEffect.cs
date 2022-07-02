using DG.Tweening;
using UnityEngine;

namespace Core.Balls {
    public sealed class BallEffect : MonoBehaviour {
        [SerializeField] BallParticleEffect BallParticleEffect;

        Color _color;
        float _scale;
        
        public void Init(Color color, float scale) {
            _color = color;
            _scale = scale;
        }

        public void PlayDieEffect() {
            var ballParticleEffect = Instantiate(BallParticleEffect, transform.position, Quaternion.identity);
            
            ballParticleEffect.Play(_color, _scale);
        }

        public void PlayHitEffect() {
            transform.parent.DOShakeScale(0.5f, 0.25f)
                .OnComplete(() => transform.parent.DORewind())
                .OnKill(() => transform.parent.DORewind());
        }
    }
}