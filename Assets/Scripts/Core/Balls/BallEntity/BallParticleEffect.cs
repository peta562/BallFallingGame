using UnityEngine;

namespace Core.Balls {
    public class BallParticleEffect : MonoBehaviour {
        [SerializeField] ParticleSystem _particleSystem;

        public void Play(Color color, float scale) {
            var main = _particleSystem.main;
            main.startColor = color;
            main.startSize = scale / 4;
            
            _particleSystem.Play();
        }
    }
}