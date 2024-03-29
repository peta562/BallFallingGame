﻿using Core.Scenes;
using UnityEngine;

namespace Core.Starters {
    public class GameStarter : MonoBehaviour {
        async void Awake() {
            ConfigureBuildSettings();
            GameContext.TryCreate();
            GameState.TryCreate();
            
            await GameState.Instance.CreateGameState();
            GoToMainMenuScene();
        }

        void GoToMainMenuScene() {
            GameContext.Instance.SceneLoader.LoadScene(SceneNames.MainMenu);
        }

        void ConfigureBuildSettings() {
            Application.targetFrameRate = 60;
        }
    }
}