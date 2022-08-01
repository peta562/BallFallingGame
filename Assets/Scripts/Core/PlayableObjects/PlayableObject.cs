using Core.Pause;
using Core.PlayableObjects.Bonuses;
using Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces;
using UnityEngine;

namespace Core.PlayableObjects {
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class PlayableObject : MonoBehaviour, IPauseHandler {
        [SerializeField] PointerDownClickHandler ClickHandler;
        [SerializeField] ParticleSystem DieParticleSystemPrefab;
        
        protected Transform _transform;
        
        protected IMoveBehaviour _moveBehaviour;
        protected IViewBehavior _viewBehavior;
        protected IParticleEffectBehavior _dieParticleEffectBehavior;
        protected IEffectBehavior _hitEffectBehavior;
        protected IOutOfBoundsBehavior _outOfBoundsBehavior;

        SpriteRenderer _spriteRenderer;
        
        public float Health { get; protected set; }
        public PlayableObjectType PlayableObjectType { get; protected set; }

        public abstract void TakeDamage(float damage);
        public abstract void DeInit();
        protected abstract void OnClicked();
        protected abstract void InitBehaviors();

        void OnEnable() {
            GameContext.Instance.PauseManager.Register(this);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = transform;
            
            ClickHandler.OnClick += OnClicked;
        }

        void OnDisable() {
            GameContext.Instance.PauseManager.UnRegister(this);
            _spriteRenderer = null;
            _transform = null;
            
            ClickHandler.OnClick -= OnClicked;
        }

        public void Move(Vector3 direction, float speed) {
            _moveBehaviour?.Move(direction, speed);
        }

        public void SetView() {
            _viewBehavior?.SetView(_spriteRenderer);
        }

        public bool CheckOutOfBounds(Vector2 stageDimensions) {
            return _outOfBoundsBehavior?.CheckOutOfBounds(stageDimensions) ?? true;
        }

        public void PlayHitEffect() {
            _hitEffectBehavior?.PlayEffect();   
        }

        public void PlayDieEffect() {
            var particleEffect = Instantiate(DieParticleSystemPrefab, transform.position, Quaternion.identity);
            _dieParticleEffectBehavior?.PlayEffect(particleEffect);
        }

        void IPauseHandler.OnPauseChanged(bool isPaused) {
            _dieParticleEffectBehavior.OnPauseChanged(isPaused);
        }
    }
}