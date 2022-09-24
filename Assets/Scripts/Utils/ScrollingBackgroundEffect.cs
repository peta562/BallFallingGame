using UnityEngine;
using UnityEngine.UI;

namespace Utils {
    public sealed class ScrollingBackgroundEffect : MonoBehaviour {
        [SerializeField] RawImage RawImage;
        [SerializeField] float X;
        [SerializeField] float Y;
        
        void Update() {
            RawImage.uvRect = new Rect(RawImage.uvRect.position + new Vector2(X, Y) * Time.deltaTime,
                RawImage.uvRect.size);
        }
    }
}