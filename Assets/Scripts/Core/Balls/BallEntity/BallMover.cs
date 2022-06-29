using UnityEngine;

namespace Core.Balls {
    public sealed class BallMover : MonoBehaviour {
        public void Move(float speed) {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}