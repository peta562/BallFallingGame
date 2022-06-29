using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Balls {
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class BallClickHandler : MonoBehaviour, IPointerDownHandler {
        public event Action OnBallClicked;

        public void OnPointerDown(PointerEventData eventData) {
            OnBallClicked?.Invoke();
        }
    }
}