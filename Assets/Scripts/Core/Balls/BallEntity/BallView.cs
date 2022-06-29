using UnityEngine;

namespace Core.Balls {
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class BallView : MonoBehaviour {
        SpriteRenderer _spriteRenderer;

        public void Init(float scale, Sprite sprite, Color color) {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = color;
            
            transform.localScale = new Vector2(scale, scale);
        }
    }
}