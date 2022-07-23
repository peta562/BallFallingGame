using Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors {
    public sealed class DOTweenScaleEffectBehavior : IEffectBehavior {
        readonly Transform _transform;

        public DOTweenScaleEffectBehavior(Transform transform) {
            _transform = transform;
        }

        public void PlayEffect() {
            _transform.DOShakeScale(0.5f, 0.25f)
                .OnComplete(() => _transform.DORewind())
                .OnKill(() => _transform.DORewind());
        }
    }
}