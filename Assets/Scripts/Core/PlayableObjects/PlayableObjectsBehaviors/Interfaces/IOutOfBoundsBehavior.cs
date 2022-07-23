using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces {
    public interface IOutOfBoundsBehavior {
        public bool CheckOutOfBounds(Vector2 stageDimensions);
    }
}