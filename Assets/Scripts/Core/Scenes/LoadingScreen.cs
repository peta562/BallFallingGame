using UnityEngine;
using UnityEngine.UI;

namespace Core.Scenes {
    public sealed class LoadingScreen : MonoBehaviour {
        [SerializeField] Image ProgressBar;
        
        public void Init() {
            gameObject.SetActive(true);
            ProgressBar.fillAmount = 0;
        }

        public void SetProgress(float progress) {
            ProgressBar.fillAmount = Mathf.MoveTowards(ProgressBar.fillAmount, progress, 3 * Time.deltaTime);
        }

        public void DeInit() {
            gameObject.SetActive(false);
        }
    }
}