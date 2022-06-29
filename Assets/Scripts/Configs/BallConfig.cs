using Core.Balls;
using Core.Balls.BallEntity;
using UnityEngine;
using Utils;

namespace Configs {
    [CreateAssetMenu(fileName = "BallConfig", menuName = "ScriptableObjects/Configs/BallConfig", order = 2)]
    public class BallConfig : ScriptableObject {
        public Ball Prefab;
        public Sprite Sprite;
        public FloatRange Scale = new FloatRange(0.2f, 2.5f);
        public FloatRange Health = new FloatRange(10f, 50f);
    }
}