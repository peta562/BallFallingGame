using Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces;
using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors {
    public sealed class SimpleMoveBehavior : IMoveBehaviour {
        readonly Transform _transform;

        public SimpleMoveBehavior(Transform transform) {
            _transform = transform;
        }
        
        public void Move( Vector3 direction, float speed) {
            _transform.Translate(direction * speed);
        }
    }
}