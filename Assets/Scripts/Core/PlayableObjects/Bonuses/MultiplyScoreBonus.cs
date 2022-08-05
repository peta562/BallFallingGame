using Core.EventBus;
using Core.EventBus.Events;
using Core.PlayableObjects.PlayableObjectsBehaviors;
using UnityEngine;

namespace Core.PlayableObjects.Bonuses {
    public sealed class MultiplyScoreBonus : PlayableObject {
        Sprite _sprite;
        float _scale;
        
        public int Multiplier { get; private set; }
        public float MultiplierTime { get; private set; }
        
        public void Init(PlayableObjectType playableObjectType, int health, Vector2 position, float scale, Sprite sprite, int multiplier, float multiplierTime) {
            PlayableObjectType = playableObjectType;
            Health = health;
            Multiplier = multiplier;
            MultiplierTime = multiplierTime;

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

        public override void TakeDamage(int damage) {
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