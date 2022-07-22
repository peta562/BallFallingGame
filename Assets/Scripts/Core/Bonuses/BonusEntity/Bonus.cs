using Core.EventBus;
using Core.EventBus.Events;
using Core.GameBehaviors.Interfaces;
using Core.Pause;
using UnityEngine;

namespace Core.Bonuses.BonusEntity {
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Bonus : MonoBehaviour, IPauseHandler {
        [SerializeField] protected PointerDownClickHandler ClickHandler;
        [SerializeField] ParticleSystem HitParticleSystemPrefab;
        
        protected Transform _transform;
        
        protected IMoveBehaviour _moveBehaviour;
        protected IViewBehavior _viewBehavior;
        protected IParticleEffectBehavior _dieParticleEffectBehavior;
        protected IEffectBehavior _hitEffectBehavior;
        protected IOutOfBoundsBehavior _outOfBoundsBehavior;

        SpriteRenderer _spriteRenderer;

        public abstract void TakeDamage(float damage);
        public abstract void DeInit();
        protected abstract void OnClicked();
        protected abstract void InitBehaviors();

        public float Health { get; protected set; }
        public BonusType BonusType { get; protected set; }

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
            _moveBehaviour.Move(direction, speed);
        }

        public void SetView() {
            _viewBehavior.SetView(_spriteRenderer);
        }

        public void CheckOutOfBounds(Vector2 stageDimensions) {
            if ( _outOfBoundsBehavior.CheckOutOfBounds(stageDimensions) ) {
                EventManager.Instance.Fire(new BonusFell(this));
            }
        }

        public void PlayHitEffect() {
            _hitEffectBehavior.PlayEffect();   
        }

        public void PlayDieEffect() {
            var particleEffect = Instantiate(HitParticleSystemPrefab, transform.position, Quaternion.identity);
            _dieParticleEffectBehavior.PlayEffect(particleEffect);
        }

        void IPauseHandler.OnPauseChanged(bool isPaused) {
            _dieParticleEffectBehavior.OnPauseChanged(isPaused);
        }
    }
}