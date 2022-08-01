using UnityEngine;

namespace Core.UI.WindowsUI {
    public sealed class WindowBackground : MonoBehaviour {
        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}