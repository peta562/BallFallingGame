using UnityEngine;

namespace Configs {
    public enum ScoreType {
        ConfigScore,
        HealthBallScore,
    }
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/Configs/GameConfig", order = 1)]
    public class GameConfig : ScriptableObject {
        public int MaxLives;
        public int BallSpeed;
        public int BonusSpeed;
        public float BallDeltaSpawn;
        public float BonusDeltaSpawn;
        public int Damage;
        public ScoreType ScoreType;
        public int Score;
    }
}