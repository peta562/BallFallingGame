using System.Collections.Generic;
using Configs;
using Core.Balls;
using Core.Score;
using UnityEngine;

namespace Core {
    public class GameBehavior : MonoBehaviour {
        [SerializeField] GameConfig  _gameConfig;
        [SerializeField] BallFactory _ballFactory;

        readonly List<BaseController> _controllers = new List<BaseController>();

        void Awake() {
            CreateControllers();
            Init();
        }

        void OnDestroy() {
            DeInit();
        }

        void CreateControllers() {
            _controllers.Add(new BallsController(_gameConfig, _ballFactory));
            _controllers.Add(new LivesController(_gameConfig));
            _controllers.Add(new ScoreController(_gameConfig));
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
    }
}