using TMPro;
using UnityEngine;
using Utils;

namespace Core.UI.LevelUI {
    public sealed class ScoreView : MonoBehaviour {
        [SerializeField] TextCounterAnimation TextCounterAnimation;
        [SerializeField] TMP_Text ScoreText;

        public void Init(int score) {
            ScoreText.text = score.ToString();
        }

        public void UpdateScore(int score) {
            TextCounterAnimation.Value = score;
        }
    }
}