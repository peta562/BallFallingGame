﻿using Core.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.MainMenuUI {
    public sealed class MainMenuUI : MonoBehaviour {
        [SerializeField] Button PlayButton;

        ProgressController _progressController;

        public void Init(ProgressController progressController) {
            _progressController = progressController;
            
            PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }

        public void DeInit() {
            PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        void OnPlayButtonClicked() {
            _progressController.TryStartLevel();
        }
    }
}