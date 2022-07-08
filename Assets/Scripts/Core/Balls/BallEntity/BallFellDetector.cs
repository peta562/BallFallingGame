using System;
using UnityEngine;

namespace Core.Balls {
    public sealed class BallFellDetector : MonoBehaviour {
        Action _onBallFell;

        public void Init(Action onBallFell) {
            _onBallFell = onBallFell;
        }

        public void DeInit() {
            _onBallFell = null;
        }
        
        public void CheckOutOfBounds(Vector2 position, float scale, Vector2 stageDimensions) {
            if ( position.y < -stageDimensions.y - scale / 2 ) {
                _onBallFell?.Invoke();
            }
        }
    }
}