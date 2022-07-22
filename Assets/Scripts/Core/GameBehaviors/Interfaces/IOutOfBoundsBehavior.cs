using UnityEngine;

namespace Core.GameBehaviors.Interfaces {
    public interface IOutOfBoundsBehavior {
        public bool CheckOutOfBounds(Vector2 stageDimensions);
    }
}