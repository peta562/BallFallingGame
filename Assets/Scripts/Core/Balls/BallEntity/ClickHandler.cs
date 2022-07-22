using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Balls {
    [RequireComponent(typeof(Collider2D))]
    public sealed class ClickHandler : MonoBehaviour, IPointerDownHandler {
        public event Action OnClick;

        bool IsPaused => GameContext.Instance.PauseManager.IsPaused;

        public void OnPointerDown(PointerEventData eventData) {
            if ( IsPaused ) {
                return;
            } 
            
            OnClick?.Invoke();
        }
    }
}