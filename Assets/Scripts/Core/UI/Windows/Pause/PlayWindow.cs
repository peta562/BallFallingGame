using Configs;
using Core.Score;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI.Windows.Pause {
    public sealed class PlayWindow : BaseWindow {
        [SerializeField] Button PlayButton;
        [SerializeField] TMP_Text TargetScoreText; 

        ScoreController _scoreController;
        LevelInfo _levelInfo;

        public void Init(ScoreController scoreController, LevelInfo levelInfo) {
            _scoreController = scoreController;
            _levelInfo = levelInfo;
        }
        
        public override void Show() {
            TargetScoreText.text = $"Your target: {_levelInfo.TargetScore}";
            PlayButton.onClick.AddListener(OnStartLevel);

            base.Show();
        }

        public override void Hide() {
            PlayButton.onClick.RemoveListener(OnStartLevel);

            base.Hide();
        }

        void OnStartLevel() {
            Hide();
            SceneManager.LoadScene("Level");
        }
    }
}