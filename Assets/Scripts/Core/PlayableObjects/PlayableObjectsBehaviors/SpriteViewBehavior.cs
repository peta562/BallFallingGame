using Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces;
using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors {
    public sealed class SpriteViewBehavior : IViewBehavior {
        readonly Sprite _sprite;

        public SpriteViewBehavior(Sprite sprite) {
            _sprite = sprite;
        }
        
        public void SetView(SpriteRenderer spriteRenderer) {
            spriteRenderer.sprite = _sprite;
        }
    }
}