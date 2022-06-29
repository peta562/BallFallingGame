using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Core {
    public class GameBehavior : MonoBehaviour {
        [SerializeField] GameConfig  _gameConfig;
        [SerializeField] BallFactory _ballFactory;

        readonly List<IController> _controllers = new List<IController>();

        void Awake() {
            CreateControllers();
            Init();
        }

        void OnDestroy() {
            DeInit();
        }

        void CreateControllers() {
            _controllers.Add(new BallsController(_gameConfig, _ballFactory));
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