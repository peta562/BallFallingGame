using UnityEngine;

namespace Configs {
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Configs/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject {
        public int Lives;
        public int Speed;
        public float BallDeltaSpawn;
        public float Damage;
    }
}