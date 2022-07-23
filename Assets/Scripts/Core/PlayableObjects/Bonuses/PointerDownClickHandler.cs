using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.PlayableObjects.Bonuses {
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class PointerDownClickHandler : MonoBehaviour, IPointerDownHandler {
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