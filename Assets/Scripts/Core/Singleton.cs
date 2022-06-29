using UnityEngine;

namespace Core {
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        public static T Instance;
        
        protected void Awake() {
            if ( Instance == null ) {
                Instance = this as T;
            }
            else if ( Instance == this ) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}