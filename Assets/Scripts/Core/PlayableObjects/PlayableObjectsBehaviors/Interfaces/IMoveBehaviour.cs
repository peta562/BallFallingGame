using UnityEngine;

namespace Core.PlayableObjects.PlayableObjectsBehaviors.Interfaces {
    public interface IMoveBehaviour {
        public void Move(Vector3 direction, float speed);
    }
}