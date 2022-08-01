using Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces;
using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors {
    public sealed class FellDownOutOfBoundsBehavior : IOutOfBoundsBehavior {
        readonly Transform _transform;
        readonly float _scale;

        public FellDownOutOfBoundsBehavior(Transform transform, float scale) {
            _transform = transform;
            _scale = scale;
        }
        
        public bool CheckOutOfBounds(Vector2 stageDimensions) {
            return _transform.position.y < (-stageDimensions.y - _scale);
        }
    }
}