using System.Collections.Generic;
using Configs;
using Core.Balls;
using Core.Score;
using Core.UI;
using UnityEngine;

namespace Core {
    public class LevelStarter : MonoBehaviour {
        [SerializeField] BallFactory _ballFactory;
        [SerializeField] LevelUI _levelUi;

        readonly List<BaseController> _controllers = new List<BaseController>();

        void Awake() {
            var gameState = GameState.Instance;
            
            AddControllers(gameState);
            Init();
            InitUI(gameState);
        }

        void OnDestroy() {
            DeInit();
            DeInitUI();
        }

        void AddControllers(GameState gameState) {

            _controllers.Add(gameState.BallsController);
            gameState.BallsController.ChangeFactory(_ballFactory);
            
            _controllers.Add(gameState.LivesController);
            _controllers.Add(gameState.ScoreController);
        }
        
        void Init() {
            foreach (var controller in _controllers) {
                controller.Init();
            }
        }

        void Update() {
            foreach (var controller in _controllers) {
                controller.Update();
            }
        }

        void DeInit() {
            foreach (var controller in _controllers) {
                controller.DeInit();
            }
        }
        
        void InitUI(GameState gameState) {
            _levelUi.Init(gameState.LivesController, gameState.ScoreController);
        }

        void DeInitUI() {
            _levelUi.DeInit();
        }
    }
}