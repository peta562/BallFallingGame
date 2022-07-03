using Core.Pause;
using UnityEngine;

namespace Core.Balls {
    public class BallParticleEffect : MonoBehaviour, IPauseHandler {
        [SerializeField] ParticleSystem _particleSystem;

        void Awake() {
            GameState.Instance.PauseManager.Register(this);
        }
        
        public void Play(Color color, float scale) {
            var main = _particleSystem.main;
            main.startColor = color;
            main.startSize = scale / 4;
            
            _particleSystem.Play();
        }

        void OnDestroy() {
            GameState.Instance.PauseManager.UnRegister(this);
        }

        void IPauseHandler.OnPauseChanged(bool isPaused) {
            var main = _particleSystem.main;
            main.simulationSpeed = isPaused ? 0f : 1f;
        }
    }
}