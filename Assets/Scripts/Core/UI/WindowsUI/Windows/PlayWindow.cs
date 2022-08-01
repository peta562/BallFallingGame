using Configs;
using Core.Level;
using Core.Scenes;
using Core.Score;
using Core.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.WindowsUI.Windows {
    public sealed class PlayWindow : BaseWindow {
        [SerializeField] Button PlayButton;
        [SerializeField] TMP_Text LevelIdText;
        [SerializeField] TMP_Text TargetScoreText; 

        ScoreController _scoreController;
        LevelController _levelController;
        LevelInfo _levelInfo;

        public void Init(ScoreController scoreController, LevelController levelController, LevelInfo levelInfo) {
            _scoreController = scoreController;
            _levelController = levelController;
            _levelInfo = levelInfo;
        }
        
        public override void Show() {
            TargetScoreText.text = $"Your target: {_levelInfo.TargetScore.ToString()}";
            LevelIdText.text = $"Current level: {_levelInfo.Id.ToString()}";
            
            PlayButton.onClick.AddListener(OnPlayButtonClicked);

            base.Show();
        }

        public override void Hide() {
            PlayButton.onClick.RemoveListener(OnPlayButtonClicked);

            base.Hide();
        }

        void OnPlayButtonClicked() {
            SoundManager.Instance.PlaySound(AudioClipNames.ButtonClick);
            Hide();
            _levelController.StartLevel(_levelInfo);
            SceneLoader.Instance.LoadScene(SceneNames.Level);
        }
    }
}