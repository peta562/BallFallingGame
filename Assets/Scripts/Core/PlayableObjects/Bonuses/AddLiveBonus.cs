using Core.EventBus;
using Core.EventBus.Events;
using Core.PlayableObjects.PlayableObjectsBehaviors;
using UnityEngine;

namespace Core.PlayableObjects.Bonuses {
    public sealed class AddLiveBonus : PlayableObject {
        Sprite _sprite;
        float _scale;
        
        public int LivesToAdd { get; private set; }
        
        public void Init(PlayableObjectType playableObjectType, float health, Vector2 position, float scale, Sprite sprite, int livesToAdd) {
            PlayableObjectType = playableObjectType;
            Health = health;
            LivesToAdd = livesToAdd;
            
            _transform.position = position;
            
            _scale = scale;
            _transform.localScale = new Vector2(scale, scale);
            
            _sprite = sprite;

            InitBehaviors();
        }

        public override void DeInit() {
            _moveBehaviour = null;
            _viewBehavior = null;
            _dieParticleEffectBehavior = null;
            _hitEffectBehavior = null;
            _outOfBoundsBehavior = null;
        }

        public override void TakeDamage(float damage) {
            Health -= damage;
        }

        protected override void InitBehaviors() {
            _moveBehaviour = new SimpleMoveBehavior(_transform);
            _viewBehavior = new SpriteViewBehavior(_sprite);
            _dieParticleEffectBehavior = new ParticleSystemWithSpriteAndScaleEffectBehavior(_sprite, _scale);
            _hitEffectBehavior = new DOTweenScaleEffectBehavior(_transform);
            _outOfBoundsBehavior = new FellDownOutOfBoundsBehavior(_transform, _scale);
        }

        protected override void OnClicked() {
            EventManager.Instance.Fire(new PlayableObjectClicked(this));
        }
    }
}