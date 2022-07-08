using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Balls {
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class BallClickHandler : MonoBehaviour, IPointerDownHandler {
        Action _onBallClicked;

        bool IsPaused => GameState.Instance.PauseManager.IsPaused;

        public void Init(Action onBallClicked) {
            _onBallClicked = onBallClicked;
        }

        public void DeInit() {
            _onBallClicked = null;
        }
        
        public void OnPointerDown(PointerEventData eventData) {
            if ( IsPaused ) {
                return;
            } 
            
            _onBallClicked?.Invoke();
        }
    }
}