using Core.EventBus;
using Core.EventBus.Events;
using Core.PlayableObjects.PlayableObjectsBehaviors;
using UnityEngine;

namespace Core.PlayableObjects.Balls {
    public sealed class Ball : PlayableObject {
        Sprite _sprite;
        Color _color;
        float _scale;

        public void Init(PlayableObjectType playableObjectType, float health, Vector2 position, float scale, Sprite sprite, Color color) {
            PlayableObjectType = playableObjectType;
            Health = health;
            
            _transform.position = position;

            _scale = scale;
            _transform.localScale = new Vector2(scale, scale);

            _sprite = sprite;
            _color = color;

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
            _viewBehavior = new SpriteAndColorViewBehavior(_sprite, _color);
            _dieParticleEffectBehavior = new ParticleSystemWithScaleAndColorEffectBehavior(_color, _scale);
            _hitEffectBehavior = new DOTweenScaleEffectBehavior(_transform);
            _outOfBoundsBehavior = new FellDownOutOfBoundsBehavior(_transform, _scale);
        }

        protected override void OnClicked() {
            EventManager.Instance.Fire(new PlayableObjectClicked(this));
        }
    }
}