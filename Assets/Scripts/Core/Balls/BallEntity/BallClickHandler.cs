using System;
using Core.Pause;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Balls {
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class BallClickHandler : MonoBehaviour, IPointerDownHandler {
        public event Action OnBallClicked;

        bool IsPaused => GameState.Instance.PauseManager.IsPaused;
        
        public void OnPointerDown(PointerEventData eventData) {
            if ( IsPaused ) {
                return;
            } 
            
            OnBallClicked?.Invoke();
        }
    }
}