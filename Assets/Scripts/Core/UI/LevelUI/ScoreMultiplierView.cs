using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.LevelUI {
    public sealed class ScoreMultiplierView : MonoBehaviour {
        [SerializeField] Slider ScoreMultiplierSlider;
        [SerializeField] TMP_Text ScoreMultiplierText;

        bool IsPaused => GameContext.Instance.PauseManager.IsPaused;
        
        bool _isMultiplyScoreEnabled;

        void Update() {
            if ( !_isMultiplyScoreEnabled || IsPaused) {
                return;
            }
            ScoreMultiplierSlider.value -= Time.deltaTime;
        }
        
        public void StartMultiplyTimer(int multiplier, float multiplierTime) {
            ScoreMultiplierText.text = $"x{multiplier.ToString()}";
            ScoreMultiplierSlider.maxValue = multiplierTime;
            ScoreMultiplierSlider.value = multiplierTime;

            _isMultiplyScoreEnabled = true;
            gameObject.SetActive(true);
        }

        public void StopMultiplyTimer() {
            _isMultiplyScoreEnabled = false;
            gameObject.SetActive(false);
        }
    }
}