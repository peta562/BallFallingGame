using UnityEngine;

namespace Core.Balls {
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class BallView : MonoBehaviour {
        SpriteRenderer _spriteRenderer;

        public void Init(Sprite sprite, Color color) {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = color;
            
            
        }
    }
}