using Core.GameBehaviors.Interfaces;
using UnityEngine;

namespace Core.GameBehaviors {
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