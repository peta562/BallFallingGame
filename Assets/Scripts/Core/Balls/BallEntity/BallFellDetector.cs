using System;
using UnityEngine;

namespace Core.Balls {
    public sealed class BallFellDetector : MonoBehaviour {
        public event Action OnBallFell;

        public void CheckOutOfBounds(Vector2 position, float scale, Vector2 stageDimensions) {
            if ( position.y < -stageDimensions.y - scale / 2 ) {
                OnBallFell?.Invoke();
            }
        }
    }
}