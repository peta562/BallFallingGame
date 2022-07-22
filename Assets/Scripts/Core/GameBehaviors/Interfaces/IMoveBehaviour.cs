using UnityEngine;

namespace Core.GameBehaviors.Interfaces {
    public interface IMoveBehaviour {
        public void Move(Vector3 direction, float speed);
    }
}